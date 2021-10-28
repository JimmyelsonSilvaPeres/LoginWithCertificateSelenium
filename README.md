
<br id="top">

# Access with Certificate Chrome in C#

This is a solution to sign in using a Digital Certificate.
First I need to thanks <a href="https://gist.github.com/IngussNeilands">IngussNeilands</a> for the tutorial provided on his Github. It saved me!
You can follow the steps on IngussNeilands´s tutorial <a href="https://gist.github.com/IngussNeilands/3bbbb7d78954c85e2e988cf3bfec7caa">here</a> or follow my version of his tutorial below.
## Steps to Configure the Policy Groups


  1. Download Chrome Policy Templates from here: [http://dl.google.com/dl/edgedl/chrome/policy/policy_templates.zip](http://dl.google.com/dl/edgedl/chrome/policy/policy_templates.zip)

  2. Extract the ```.zip``` file and find the ```chrome.adm``` that matches the country and the language settings on your Windows, following the path: 
  ```
  policy_templates\windows\adm\<YourCountryAndLanguage>\chrome.adm
  ```

  3. Type ```"run"``` into your _Windows Search Bar_ or press ```Windows + R```. Then type de command ```gpedit.msc``` to open the _The Local Group Policy Editor_

  4. Now, access: 
  ```
  Computer Policy > Computer Configuration
  ``` 
  5. Right-click the file ```Administrative Templates``` and select **Add or remove templates**

  6. Click ```add``` and navigate to the ```chrome.adm``` that you choose before on  **policy_templates\windows\adm\<YourCountryAndLanguage>\chrome.adm**. Click to open it

  7. Now, navigate to:
   ```
   Computer Policy > Computer Configuration > Administrative Templates > Classic Administrative Templates(ADM) > Google > Google Chrome > Content Settings
   ```

  8. Then on the right side of the window, find and double-click the option  "_**Automatically select client certificates for these sites**_"

  9. Click the **Enabled** option

  10. Now, Click **Show...** in the option pane below

  11. Copy and paste the ```JSON``` below in the line of the column Value:
  ```
  {"pattern":"https://[*.]example.com","filter":{"ISSUER":{"CN":"example.com"}, "SUBJECT":{"CN":"value"}}
  ```
  12. This JSON needs to be rewritten with your **certificate informations**

  13. Press again ```Windows + R``` then type de command ```regedit``` to open the _Regisry Editor_ and navigate to the following path: 
  ```
  HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome\AutoSelectCertificateForUrls
  ``` 
  14. Check if there is a registry with the name ```1``` created

  15. If a registry with the name ```1``` was created, you can now Run the application!
  
  16. Else, you'll need to either change de name of the registry to ```1``` or change the parameter of the method **Registry.LocalMachine.OpenSubKey().SetValue()** in the application to the name of the registry you just created.




<h2><strong>How to rewrite the Chrome Configure JSON</strong></h2>
<p>Ok, now a will give you a brief explanation on how to rewrite the Chrome Config JSON.</p>
<p>In the <b>"pattern"</b> key the value needs to be the <b>URL</b> that the certificate will be <b>sent to</b>. In most cases this URL is the same URL of the page, but some sites won't use the same URL base to send the certificate.
For example, when I was trying to <i><b>Web Scraping</b></i> the NFS-e in <i>"Uberlândia city"</i> I needed to debug the script of the page to find the URL to where the certificate was sent.</p>
<p>The <b>"filter"</b> key will have the certificate information. In my case, I need to access the same website with different certificates, and for doing that I'll have to fill the JSON with the information of <b>"ISSUER"</b> and the <b>"SUBJECT"</b>. <b><i>Chrome will choose one certificate that matches with the information content in the filter key</i></b>. For example, if I fill the <b>"CN"</b> from the <b>"ISSUER"</b> object with "<i>SERASA Certificadora Digital v5</i>" I'll have <b>more than one</b> certificate with these informations and <b><i>Chrome won't be able to choose the right certificate</i></b>.</p>
<p>If you went through all these steps, you can now run your application!</p>
<p><strong>Don´t forget</strong> to fill the <b>"List&ltstring&gtcertifiedList"</b> with the <b>path</b> and the <b>password</b> of all the certificates that you have installed.</p>

<br>

> [Back to Top](#top) ☝️
