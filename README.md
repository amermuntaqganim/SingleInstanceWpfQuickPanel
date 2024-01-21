This is an example code of making a WPF appliaction behave like single instance.
If user run exe once and or run full trust from uwp application (e.g. desktop bridge scenario) once
existing running exe will always work for multiple exe running or full trust launch.

* Here wpf will have a system tray.
* User can click tray icon to show/hide quick panel view just above the tray icon.
* Maintaining a Single Instance wpf exe.
* Quick Panel window will not have any title bar.
* Though window will not shown in taskbar existing window will be shown after clicking tray icon.
