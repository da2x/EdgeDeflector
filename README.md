# EdgeAndBingDeflector

*EdgeAndBingDeflector* is  a small helper application that intercepts URIs that force-open web links in Microsoft Edge and redirects it to the system’s default web browser. This allows you to use Windows features like the Cortana assistant and built-in help links with the browser of your choice instead of being forced to use Microsoft Edge.

It can also convert Bing searches to other engines. Currently, Google and DuckDuckGo are supported.

You’ll never see EdgeAndBingDeflector ever again after installing it. It does its thing transparently in the background and only runs when a link needs to be deflected away from Microsoft Edge.

System requirements: Windows 10, and your favorite web browser and search engine.

Read more about [how EdgeDeflector works](https://ctrl.blog/entry/edgedeflector-default-browser) and why it was created.

# Installation

  1. **Download** the latest version of EdgeAndBingDeflector from [GitHub releases](https://github.com/radialapps/EdgeAndBingDeflector/releases)
  2. Run the installer once to configure your system
  3. You will be asked whether you want to divert your searches to Google or DuckDuckGo.

You may need to repeat the above steps after installing major feature updates to Windows through Windows Update.

If you dismiss the dialog, or choose the wrong app; you can apply the change  in the Windows Settings  app: Apps: Default apps: Choose default apps by protocol: microsoft-edge.

If EdgeDeflector isn’t listed in either locations, try running it one more time; or lastly restarting your PC before you try again. (This last option actually helps if the System Registry is locked up or Windows is being suborn.)

You don’t need to specify your browser of choice in EdgeDeflector. It will pick up on the system configured default from Windows Settings app: Apps: Default apps: Web browser.
