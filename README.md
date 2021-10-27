# Access with Certificate Chrome in C#

This is a solution to sign in using a Digital Certificate.
First I need to thanks <a href="https://gist.github.com/IngussNeilands">IngussNeilands</a> for the tutorial provided on his Github. It saved me!
You can follow the steps on IngussNeilands´s tutorial <a href="https://gist.github.com/IngussNeilands/3bbbb7d78954c85e2e988cf3bfec7caa">here</a> or follow my version of his tutorial below.
## Steps to Configure the Policy Groups


  1. Download Chrome Policy Tamplates from here: [http://dl.google.com/dl/edgedl/chrome/policy/policy_templates.zip](http://dl.google.com/dl/edgedl/chrome/policy/policy_templates.zip)

  2. Extract the ```.zip``` file and find the ```chrome.adm``` that matches the country and the language settings on your Windows, following the path: ```policy_templates\windows\adm\<YourCountryAndLanguage>\chrome.adm```

  3. Type ```"run"``` into your Windows Search Bar or press ```Windows + R```. Then type de command ```gpedit.msc``` to open the _The Local Group Policy Editor_

  4. Now, access: ```'Computer Policy>> Computer Configuration'``` and right-click the file ```'Administrative Templates'``` and select ```'Add or remove tamplates'```

  5. Click ```'add'``` and navigate to the ```chrome.adm``` that you choose before on  ```'policy_templates\windows\adm\<YourCountryAndLanguage>\chrome.adm'```. Click to open it

  6. Now, navigate to:
   ```'Computer Policy>> Computer Configuration>> Administrative Templates>> Classic Administrative Templates(ADM)>> Google>> Google Chrome>> Content Settings'```

  7. Then on the rigth side of the window find and double-click the option ```'Automatically select client certificates for these sites'```

  8. Click the ```'Enabled'``` option

  9. Now, Click the ```'Show...'``` in the option pane below

  10. Copy and paste the ```'JSON'``` below in the line of the column Value:
  ```{"pattern":"https://[*.]example.com","filter":{"ISSUER":{"CN":"example.com"}, "SUBJECT":{"CN":"value"}}```. This JSON needs to be rewriten with your certificate informations

  11. Press again ```'Windows + R'``` then type de command ```'regedit'``` to open the _Regisry Editor_ and navagate to the following path: ```'HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome\AutoSelectCertificateForUrls'``` and check if a registry with the name ```'1'``` was created

  12. If a registry with the name ```'1'``` was created, you can now Run the aplication!
  
  13. Else, you'll need to change de name of the registry to ```'1'``` or change the parameter of the method ```Registry.LocalMachine.OpenSubKey().SetValue()``` in the application to the name of your just created registry.




<h2 ><strong>How to rewrite the Chrome Configure JSON</strong></h2>
<p>Ok, now a will give you a brief explanation on how to rewrite the Chrome Config JSON.</p>
<p>In the "pattern" key the value needs to be the URL that the certificate will be sent to. In most cases this URL is the same URL of the page, but some sites don´t use the same URL base to send the certificate.
For example, when I was trying to <i>webscraping</i> the NFS-e in Uberlândia city I needed to debug the script of the page to find the URL to where the certificate was sent.</p>
<p>The "filter" key will have the certificate information. In my case, I need to access the same website with diferent certificates, for that I'll have to fill the JSON with the information of "ISSUER" and "SUBJECT". Chrome will choose one certificate that matches with the informations content in the filter key. For example, if I fill the "CN" from "ISSUER" object with "SERASA Certificadora Digital v5" I'll have more than one certificate with these informations and Chrome won´t be able to choose the right certificate.</p>
<p>If you went through all these steps, you can now probably run your application.</p>
<p>Don´t forget to fill the "List&ltstring&gtDcertifiedList" with the path and the password of all the certificates that you have installed.</p>
