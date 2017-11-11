# EdgeAndBingDeflector

*EdgeAndBingDeflector* is  a small helper application that intercepts URIs that force-open web links in Microsoft Edge and redirects it to the system’s default web browser, along with changing the search engine to Google or DuckDuckGo if necessary. This allows you to use Windows features like the Cortana assistant and built-in help links with the browser of your choice instead of being forced to use Microsoft Edge. With EdgeAndBingDefelctor, you’re free to use Firefox, Google Chrome, along with your favorite search engine!

You’ll never see EdgeAndBingDeflector ever again after installing it. It does its thing transparently in the background and only runs when a link needs to be deflected away from Microsoft Edge.

System requirements: Windows 10, and your favorite web browser.

Read more about [how EdgeAndBingDeflector works](https://www.ctrl.blog/entry/edgedeflector-default-browser) and why it was created.

# Installation

  1. **Download** the latest version of EdgeAndBingDeflector from [GitHub releases](https://github.com/radialapps/EdgeAndBingDeflector/releases)
  2. Run the installer once to configure your system

You may need to **repeat the above steps after installing major feature updates to Windows** through Windows Update. You can subscribe to the [AppCast feed](https://github.com/radialapps/EdgeAndBingDeflector/releases.atom) to be notified of any new releases.

If you dismiss the initial dialog to choose to use EdgeAndBingDeflector, or choose the wrong app; you can apply the change  in the Windows Settings app: Apps: Default apps: Choose default apps by protocol: microsoft-edge.

If EdgeAndBingDeflector isn’t listed in either locations, try running it one more time; or lastly restarting your PC before you try again. (This last option actually helps if the System Registry is locked up or Windows is being stubborn.)

You don’t need to specify your browser of choice in EdgeAndBingDeflector. It will pick up on the system configured default from Windows Settings app: Apps: Default apps: Web browser.

# FAQ

## “Intercepting links” sounds like it would affect my privacy?

Yes it does, but no. Everything is done on your local computer. EdgeAndBingDeflector rewrites links which are forced by the Windows shell to open inside Microsoft Edge to open using your default web browser instead. No data is collected about you nor even stored on your local computer.

## How do I uninstall EdgeAndBingDeflector?

By following [these instructions](https://github.com/da2x/EdgeDeflector/wiki/Uninstall).