Sitecore Security

This module contains the following functionalities:

* Removal of security headers
* Addition of the Content Security Policy (https://www.owasp.org/index.php/Content_Security_Policy)
* Addition of X-Frame-Options header
* XDT to create a redirect + addition of HSTS header 
* XDT to configure security headers

todo:
* Script to create certificates to configure a local HTTPS site

prerequisites:
* URL Rewrite module

1) make sure unicorn is installed (Unicorn.Web may be used for this)
2) Sync the serialized items with unicorn
3) install Security.Web
4) execute the following powershell:
5) add the CSP Link template to your (basic) page template(s)
6) Create a new CSP or use  the default one


