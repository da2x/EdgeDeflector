# EdgeAndBingDeflector

*EdgeAndBingDeflector* is  a small helper application that intercepts URIs that force-open web links in Microsoft Edge and redirects it to the system’s default web browser, along with changing the search engine to Google. This allows you to use Windows features like the Cortana assistant and built-in help links with the browser of your choice instead of being forced to use Microsoft Edge. With EdgeAndBingDeflector, you’re free to use Firefox, Google Chrome, or whatever your favorite web browser with Google search engine!

You’ll never see EdgeAndBingDeflector  ever again after installing it. It does its thing transparently in the background and only runs when a link needs to be deflected away from Microsoft Edge.

System requirements: Windows 10, and your favorite web browser.

Read more about [how EdgeAndBingDeflector works](https://www.ctrl.blog/entry/edgedeflector-default-browser.html) and why it was created.

# Installation

  1. **Download** the latest version of EdgeAndBingDeflector from [GitHub releases](https://github.com/da2x/EdgeDeflector/releases)
  2. Run the installer.
  3. The installer will open a page that guides you through some manual installation steps.

You may need to **repeat the above steps after installing major feature updates to Windows** through Windows Update.

You don’t need to specify your browser of choice in EdgeAndBingDeflector. It will pick up on the system configured default from Windows Settings app: Apps: Default apps: Web browser.

# FAQ

## It isn’t working!

Please reinstall and follow the insutrctions on the page that opens after the installer has finished.

## “Intercepting links” sounds like it would affect my privacy?

Yes it does, but no. Everything is done on your local computer. EdgeAndBingDeflector rewrites links which are forced by the Windows shell to open inside Microsoft Edge to open using your default web browser instead. No data is collected about you nor even stored on your local computer.

## Will EdgeAndBingDeflector redirect Bing searches to Google?

Yes, actually that's why I created this project by forking [EdgeDeflector](https://github.com/da2x/EdgeDeflector)!

## How do I uninstall EdgeAndBingDeflector?

From Add and Remove Programs in the Windows Settings app.

**NOTE: the program name is `EdgeDeflector` (not `EdgeAndBingDeflector`).**
