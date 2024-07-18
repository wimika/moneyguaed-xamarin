using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidTestApp.Models;
using Newtonsoft.Json;

namespace AndroidTestApp
{
    [Activity(Label = "Enable Moneyguard", Theme = "@style/AppTheme.NoActionBar")]
    public class EnableMoneyguardActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.enable_moneyguard_activity);

            var btnInstallMoneyguard = FindViewById<Button>(Resource.Id.btn_install_moneyguard);
            btnInstallMoneyguard.Click += InstallMoneyGuard;

            var txtAccNumbersCovered = FindViewById<TextView>(Resource.Id.txt_acc_numbers_covered);
            //check if customer has active policy - if policy exists, proceed with installation
            var accountsCoveredResp = GetAccountsWithActivePolicy().Result;
            if(accountsCoveredResp.Policies.Count > 0)
            {
                txtAccNumbersCovered.Text = string.Join(",", accountsCoveredResp.Policies.Select(account => account.CoveredAccountId.ToString()));
            }
            else
            {
                //if not, purchase policy and proceed with installation
            }
        }

        private async Task<AccountCovered> GetAccountsWithActivePolicy()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://moneyguardservice.azurewebsites.net/api/v1/policy/all-active-policies");
                request.Headers.Add("Accept", "application/json");
                var response = await client.SendAsync(request).ConfigureAwait(false);
                
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                var accountsCovered = JsonConvert.DeserializeObject<AccountCovered>(jsonString);
                return accountsCovered;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async void InstallMoneyGuard(object sender, EventArgs eventArgs)
        {
            await Wimika.MoneyGuard.Application.MoneyGuardApp.Install();
        }
    }
}