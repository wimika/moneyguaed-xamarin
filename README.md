

## MoneyGuard SDK For Xamarin Sample

MoneyGuard SDK For Xamarin allows financial institutions to embed [Moneyguard](https://wimika.ng/moneyguard/) into
their Xamarin applications. 

## Getting Started

1. Obtain a partner Id from Wimika RMS Technologies

2. Implement a REST API endpoint that exposes [Wimika Partner Bank Service API](https://wimika.gitbook.io/wimika-partner-bank-api-documentation/)

3. Embed Wimika Moneyguard in your Android Application

## Usage For Android

### 1) Implement IBasicClient interface 
The IBasicClient interface defines methods that an embedder can use to receive callbacks from the
Moneyguard SDK. 
   
```java
using Com.Wimika.MoneyguardCore

public class BasicClientImpl : IBasicClient
{

        public void OnCredentialScanCompleted(long p0, CredentialScanResult p1)
        {
            throw new NotImplementedException();
        }

        public void OnSessionCreated(IBasicSession p0)
        {
            throw new NotImplementedException();
        }

        public void OnSessionExpired(string p0)
        {
            throw new NotImplementedException();
        }

        public void OnTransactionCheckCompleted(long p0, TransactionCheckResult p1)
        {
            throw new NotImplementedException();
        }

        public void OnTypingProfileMatchResult(long p0, TypingProfileMatchingResult p1)
        {
            throw new NotImplementedException();
        }
}

```

### 2) Initialize MoneyGuard 

Initialize Moneyguard. An IBasicSession is an implementation of the methods that support the following Moneyguard
functionality :
 - Credential Compromise Check
 - Create a Typing Profile Recorder
 - Preview a banking transaction before it is processed

It will be provided in a callback to the OnSessionCreated method of your IBasicClient implementation
```java

Activity activity; //Main Activity
var partnerBankId = <partner-bank-id>; //obtained from Wimika
var sessionToken = <session-token>;//session token that will be passed to Partner Bank REST Service to validate user session

var client = new BasicClientImpl();//create an instance of your IBasicClient implementation
MoneyguardSdk.Register(activity, partnerBankId, sessionToken, client);

```





