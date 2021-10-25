# AcessWithCertifiedChromeCSharp
One solution for who want´s login with digital certificate.
I need firth to thank <a href="https://gist.github.com/IngussNeilands">IngussNeilands</a> for the tutorial in his git thats save me.
You can just fallow the IngussNeilands´s tutorial <a href="https://gist.github.com/IngussNeilands/3bbbb7d78954c85e2e988cf3bfec7caa">here</a> or see my one version of him tutorial below:
<ol>
  <li>Downloads the Chrome policies tamplates in <a>http://dl.google.com/dl/edgedl/chrome/policy/policy_templates.zip</a> </li>
  <li>Extract and find the file chrome.adm in the path that match´s you country and language in path policy_templates\windows\adm\"Your Country - Language configure in you Windows"\chrome.adm</li>
  <li>Type it "run" in you windows search or press Windos + R and run "gpedit.msc"</li>
  <li>Rigth click in  Administrative Templates in path Computer Policy > Computer Configuration, then select "Add or remove tamplates"</li>
  <li>Click in add and select the chrome.adm that you find in policy_templates\windows\adm\"Your Country - Language configure in you Windows"\chrome.adm</li>
  <li>Now navigate to Computer Policy -> Computer Configuration -> Administrative Templates -> Classic Administrative Templates(ADM) -> Google -></li>
  <li>Press again Windows + R then type it "regedit", navagate to the path "HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome\AutoSelectCertificateForUrls" and verify if a registry with name "1" was created</li>
  <li>If a registry with name "1" was created now you can run the aplication, else you need to change de name of registry to "1" or change the parameter of method Registry.LocalMachine.OpenSubKey().SetValue() to the name of your created registry.</li>
</ol>

Ok, now a will give a brief explanation of how works the selection of a certificate with Chrome. If 
