[![Maintainability](https://cloud.quality-gate.com/dashboard/api/badge?projectName=ACP-DA2EDGTOR&branchName=master)](https://cloud.quality-gate.com/dashboard/branches/567131#overview)

# EdgeDeflector

*EdgeDeflector* is  a small helper application that intercepts URIs that force-open web links in Microsoft Edge and redirects it to the system’s default web browser. This allows you to use Windows features like the Cortana assistant and built-in help links with the browser of your choice instead of being forced to use Microsoft Edge. With EdgeDeflector, you’re free to use Firefox, Google Chrome, or whatever your favorite web browser might be!

You’ll never see EdgeDeflector ever again after installing it. It does its thing transparently in the background and only runs when a link needs to be deflected away from Microsoft Edge.

System requirements: Windows 10, and your favorite web browser.

Read more about [how EdgeDeflector works](https://www.ctrl.blog/entry/edgedeflector-default-browser.html) and why it was created.

# Installation

  1. **Download** the latest version of EdgeDeflector from [GitHub releases](https://github.com/da2x/EdgeDeflector/releases)
  2. Run the installer.
  3. The installer will open a page that guides you through some manual installation steps.

You may need to **repeat the above steps after installing major feature updates to Windows** through Windows Update.

You don’t need to specify your browser of choice in EdgeDeflector. It will pick up on the system configured default from Windows Settings app: Apps: Default apps: Web browser.

# FAQ

## It isn’t working!

Please reinstall and follow the insutrctions on the page that opens after the installer has finished.

## Will searches inside Cortana still use Bing?

Yes. EdgeDeflector doesn’t interfere with either Cortana or the Windows shell in any way. All that EdgeDeflector does is intercept links as you open them in order to rewrite them to open with your preferred web browser.

## “Intercepting links” sounds like it would affect my privacy?

Yes it does, but no. Everything is done on your local computer. EdgeDeflector rewrites links which are forced by the Windows shell to open inside Microsoft Edge to open using your default web browser instead. No data is collected about you nor even stored on your local computer.

## Will EdgeDeflector redirect Bing searches to Google?

[No.](https://github.com/da2x/EdgeDeflector/wiki/Not-replacing-your-search-engine) You can use an extension in your favorite web browser to achieve this.

## How do I uninstall EdgeDeflector?

From Add and Remove Programs in the Windows Settings app.
