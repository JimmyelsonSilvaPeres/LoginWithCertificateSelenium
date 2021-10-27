# AcessWithCertifiedChromeCSharp
One solution for who want´s login with digital certificate.
I need firth to thank <a href="https://gist.github.com/IngussNeilands">IngussNeilands</a> for the tutorial in his git thats save me.
You can just fallow the IngussNeilands´s tutorial <a href="https://gist.github.com/IngussNeilands/3bbbb7d78954c85e2e988cf3bfec7caa">here</a> or see my one version of him tutorial below.
<h2 ><strong>Steps to configure the Policys Group </strong></h2>
<ol>
  <li>Download the Chrome policies tamplates in <a>http://dl.google.com/dl/edgedl/chrome/policy/policy_templates.zip</a> </li>
  <li>Extract and find the file chrome.adm in the path that match´s you country and language in path policy_templates\windows\adm\"Your Country - Language configure in you Windows"\chrome.adm</li>
  <li>Type it "run" in you windows search or press Windos + R and run "gpedit.msc"</li>
  <li>Rigth click in  Administrative Templates in path Computer Policy > Computer Configuration, then select "Add or remove tamplates"</li>
  <li>Click in add and select the chrome.adm that you find in policy_templates\windows\adm\"Your Country - Language configure in you Windows"\chrome.adm</li>
  <li>Now navigate to Computer Policy -> Computer Configuration -> Administrative Templates -> Classic Administrative Templates(ADM) -> Google -> Google Chrome -> Content Settings</li>
  <li>Then on rigth side of the window find and double-click on the option "Automatically select client certificates for these sites"</li>
  <li>Click in the "Enabled"</li>
  <li>Click in "Show" in the option pane below</li>
  <li>Copy and paste this json {"pattern":"https://[*.]example.com","filter":{"ISSUER":{"CN":"example.com"}, "SUBJECT":{"CN":"value"}} on the "value" column. This json need to be rewriten with your one certificate informations.</li>
  <li>Press again Windows + R then type it "regedit", navagate to the path "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome\AutoSelectCertificateForUrls" and verify if a registry with name "1" was created</li>
  <li>If a registry with name "1" was created now you can run the aplication, else you need to change de name of registry to "1" or change the parameter of method Registry.LocalMachine.OpenSubKey().SetValue() in the application to the name of your created registry.</li>
</ol>
<h2 ><strong>How to rewrite the Chrome Configure Json</strong></h2>
<p>Ok, now a will give a brief explanation of how do you rewriten the Chrome Config Json.</p>
<p>In "pattern" key the value need to be the url that the certificate will be send, in the most cases the url is the same url of the page, but some sites don´t use the same url base to send the certificate, for example when I was trying to webscraping the NFS-e in Uberlandi city I need to debug the script of the page to find the url that the certificate was send.</p>
<p>The "filter" key will have the certificate information. In my case I need to access the same site with diferents certificates, so I fill the json with the information of "ISSUER" and "SUBJECT". Chrome will choose one certificate that match the informations content in the filter key. For example if you write only the "CN" in the "ISSUER" object with "SERASA Certificadora Digital v5" and you have more the one certificate with this info Chrome won´t be able to choose the certificate.</p>
<p>If you pass for all the steps now you can run teh application.</p>
<p>Don´t forget to fill the "List<string> certifiedList" with you path and password of all the certificates that you have instaled.</p>
 
