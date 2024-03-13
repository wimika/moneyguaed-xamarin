

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

Sample project in this repository calls MoneyGuardSdk.Register [here](https://github.com/wimika/moneyguard-xamarin/blob/494acf2e78b0b0fd402d7a260935b77184d5e6d9/XamarinAndroidProject/MainActivity.cs#L51)

### 3) Credential Compromise Check

A compromised user credential can lead to account takeover with severe adverse financial consequences for an account holder.
Moneyguard SDK supports checking a users credentials for existence of compromise. If such compromise is detected it is strongly advised to
suggest that users change their passwords. The code fragment below shows example of how to use MoneyGuard to check for credential compromise.

```java

...
using Wimika.MoneyGuard.Core.Types;



var credential = new Credential
{
   HashAlgorithm = HashAlgorithm.SHA256, //SHA-256 Algorithm for example
   PasswordFragmentLength = StartingCharactersLength.FOUR, //how many characters from beginning of password to be hashed
   PasswordStartingCharactersHash = "<hash>", //HashAlgorithm hash of first number of characters in PasswordFragmentLength
   Domain = "<domain>", //Domain for which credential is to be checked
   Username = "<username>", //Username to be checked
}

Task<CredentialScanResult> credentialScanResult =  await session.CheckCredential(credential);

//handle results
var status = credentialScanResult.Status;
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
Sample project in this repository performs Credential Compromise Check [here](https://github.com/wimika/moneyguard-xamarin/blob/494acf2e78b0b0fd402d7a260935b77184d5e6d9/XamarinAndroidProject/CredentialCheckActivity.cs#L63)

### 4) Typing Profile Check

Moneyguard SDK supports determining the identity of a mobile app user by obtaining a record of how a user types. The process entails an initial
period where the user enrolls their typing profile. The actual mechanism requires MoneyGuard SDK clients to attach event handlers to monitor KeyDown and TextChanged events from a entry widget where user will type a specific piece of text. Example usage is shown below.

```java

...
using Wimika.MoneyGuard.Core.Types;

//get Typing profile recorder
var typingProfileRecorder = session.TypingProfileRecorder;

//get the typing fragment to display to the user
var typingFragment = typingProfileRecorder.TypingFragment;


//attach event handlers to the entry widget that will accept the user's typing of the fragment
//When a key down event is detected call ->

typingProfileRecorder.OnKeydown();

//When text changes call ->
typingProfileRecorder.OnTextChanged(newText);

//when the user has completed typing the fragment (note that typed fragment MUST match typingProfileRecorder.TypingFragment) call ->

var typingProfileMatchingResult = await typingProfileRecorder.MatchTypingProfile();

//handle result
if(typingProfileMatchingResult.IsEnrolledOnThisDevice){
   if(typingProfileMatchingResult.Matched){
      if(typingProfileMatchingResult.HighConfidence){
          //typing profile matched with high confidence
          //proceed
      }
      else{
         //typing profile matched with low confidence
          //proceed with caution
      }
   }
   else{
      //typing profile did not match, do not proceed
   }
}
else{
   //typing profile enrollment not completed on this device
   //proceed with caution
}

```
Sample project in this repository performs Typing Profile Check [here](https://github.com/wimika/moneyguard-xamarin/blob/494acf2e78b0b0fd402d7a260935b77184d5e6d9/XamarinAndroidProject/TypingProfileMatchingActivity.cs#L61)

### 5) Debit Transaction Check

MoneyGuard SDK supports the ability to monitor debit transaction patterns as well as mobile app usage risks that may indicate the likelihood of exposure to cyberfraud. The Moneyguard App is required to provide the capture of the usage risks due to device, network , application and user attack surfaces. Example of Debit transaction check is shown below


```java

...
using Wimika.MoneyGuard.Core.Types;

var transaction = new DebitTransaction
{
    SourceAccountNumber = "CHK-123456789", //Account to debit
    Amount = 100000,
    Memo = "Electricity Purchase",
    DestinationBank = "098", // Destination Bank Code
    DestinationAccountNumber = "KGD-987654321" //Destination Account Number
};

var debitTransactionCheckResult = await session.CheckDebitTransaction( transaction);


//handle results
foreach(var specificRisk in debitTransactionCheckResult.Risks){

     if(status == RiskStatus.RISK_STATUS_WARN || status == RiskStatus.RISK_STATUS_UNKNOWN)
     {
        //warn user and proceed with caution
     }
     else if(status == RiskStatus.RISK_STATUS_UNSAFE)
     {
        //do not proceed
        break;
     }
     else
     {
         //proceed
     }
}



```

Sample project in this repository performs Debit Transaction Check [here](https://github.com/wimika/moneyguard-xamarin/blob/494acf2e78b0b0fd402d7a260935b77184d5e6d9/XamarinAndroidProject/DebitCheckActivity.cs#L47)

### 6) Get Risk Profile

This method is similar to Debit Check except that it can be called at any time to check the risk profile of the current session before permitting a sensitive operation.


```java

...
using Wimika.MoneyGuard.Core.Types;


var result = await session.GetRiskProfile();


//handle results
foreach(var specificRisk in result.Risks){

     if(status == RiskStatus.RISK_STATUS_WARN || status == RiskStatus.RISK_STATUS_UNKNOWN)
     {
        //warn user and proceed with caution
     }
     else if(status == RiskStatus.RISK_STATUS_UNSAFE)
     {
        //do not proceed
        break;
     }
     else
     {
         //proceed
     }
}



```
Sample project in this repository performs GetRiskProfile [here](https://github.com/wimika/moneyguard-xamarin/blob/494acf2e78b0b0fd402d7a260935b77184d5e6d9/XamarinAndroidProject/ChoosingActivity.cs#L46)
