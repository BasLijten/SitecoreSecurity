Sitecore Security

This module contains the following functionalities:

* Removal of security headers
* Addition of the Content Security Policy (http://blog.baslijten.com/sitecore-security-3-prevent-xss-using-content-security-policy/, https://www.owasp.org/index.php/Content_Security_Policy)
* Addition of X-Frame-Options header, based on CSP settings
* Addition of X-XSS-Protection header, based on CSP settings
* Addition of X-Content-Type-Options header
* Addition of lets's encrypt options for Sitecore (http://blog.baslijten.com/sitecore-security-4-serve-your-site-securely-over-https-with-lets-encrypt/)
* XDT to create a redirect + addition of HSTS header (http://blog.baslijten.com/sitecore-security-2-secure-connections-and-how-to-force-the-browser-to-use-the-secure-connection/)
* XDT to configure security headers (https://www.akshaysura.com/2016/08/19/secure-sitecore-secure-headers-xss-protection/)
* XDT to set secure cookies
* XDT to set secure forms authentiction cookies
* Extension method to Rotate Session ID's -> when used during login and logout, Session ID is changed, this helps against session-fixation attacks. Todo: copy Session from old to new
* Addition of stronger password hashing algorithm (http://blog.baslijten.com/sitecore-security-1-how-to-replace-the-password-hashing-algorithm/) 

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
7) use the XDT to transform your web.config 


