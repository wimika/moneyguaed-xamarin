

## MoneyGuard SDK For Xamarin Sample

MoneyGuard SDK For Xamarin allows financial institutions to embed [Moneyguard](https://wimika.ng/moneyguard/) into
their Xamarin applications. 

## Getting Started

1. Obtain a partner Id from Wimika RMS Technologies

2. Implement a REST API endpoint that exposes [Wimika Partner Bank Service API](https://wimika.gitbook.io/wimika-partner-bank-api-documentation/), provide your service url to Wimika RMS Technologies

3. Embed Wimika Moneyguard in your Android Application

## Usage For Android

### 1) Add Nuget Packages to Your Project
- Shared Xamarin Project - (Wimika.MoneyGuard.Core.Types) ![NuGet Version](https://img.shields.io/nuget/v/Wimika.MoneyGuard.Core.Types)
- Android Project - (Wimika.MoneyGuard.Core.Android) ![NuGet Version](https://img.shields.io/nuget/v/Wimika.MoneyGuard.Core.Android)


### 2) Initialize MoneyGuard 

Initialize Moneyguard. An IBasicSession is an implementation of the methods that support the following Moneyguard
functionality :
 - Obtain an authorization token for MoneyGuard REST API service calls
 - Credential Compromise Check
 - Create a Typing Profile Recorder
 - Preview a banking debit transaction before it is processed

```java

...
using Wimika.MoneyGuard.Core.Android;

Activity activity; //Main Activity
int partnerBankId = <partner-bank-id>; //obtained from Wimika
string sessionToken = <session-token>;//session token that will be passed to Partner Bank REST Service to validate user session 

Task<IBasicSession> session = await MoneyguardSdk.Register(activity, partnerBankId, sessionToken);

...

var moneyGuardAuthToken = session.SessionId;

//use moneyGuardAuthToken in REST API call using header "Authorization" : "Bearer  <moneyGuardAuthToken>"

```

Sample project in this repository calls MoneyGuardSdk.Register [here](https://github.com/wimika/moneyguard-xamarin/blob/0392cb15d9e19683e411f72bed1a70271dbc74d5/MoneyGuardSdkExample/MoneyGuardSdkExample.Android/MainActivity.cs#L27)

### 3) Credential Compromise Check

A compromised user credential can lead to account takeover with severe adverse financial consequences for an account holder.
Moneyguard SDK supports checking a users credentials for existence of compromise. If such compromise is detected it is strongly advise to
suggest that users change their passwords. The code fragment below shows example of how to use MoneyGuard to check for credential compromise.

```java

...
using Wimika.MoneyGuard.Core.Types;

var credential = new Credential
{
   HashAlgorithm = HashAlgorithm.SHA256, //SHA-256 Algorithm
   PasswordFragmentLength = StartingCharactersLength.FOUR, //how many characters from beginning of password to be hashed
   PasswordStartingCharactersHash = <hash>, //HashAlgorithm hash of first number of characters in PasswordFragmentLength
   Domain = credential.Domain, //Domain for which credential is to be checked
   Username = credential.Username, //Username to be checked
}

Task<CredentialScanResult> credentialScanResult =  await session.CheckCredential(credential);

//handle results
var status = credentialScanResult;
if(status == RiskStatus.RISK_STATUS_WARN || status == RiskStatus.RISK_STATUS_UNKNOWN)
{
   // warn user that their credentials may be compromised and they are strongly advised to
   //change their passowrd
}
else if(status == RiskStatus.RISK_STATUS_UNSAFE)
{
   //alert user that their credentials are compromised. Require user to change their password before
   //permitting any system access
}
else
{
    //proceed
}

```

### 4) Typing Profile Check

### 5) Debit Transaction Check








