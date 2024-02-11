using Microsoft.Win32;
using TweakerWin.TweakHelper;

namespace TweakerWin.Tweaks
{
    internal class VisualTweak
    {
        /// <summary>
        /// // Устанавливает обработчики контекстного меню "Копировать в" и "Переместить в".
        /// </summary>
        public void EnableCopyToMoveContextMenu()
        {
            Registry.SetValue("HKEY_CLASSES_ROOT\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", "", "{C2FBB630-2971-11D1-A18C-00C04FD75D13}");
            Registry.SetValue("HKEY_CLASSES_ROOT\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", "", "{C2FBB631-2971-11D1-A18C-00C04FD75D13}");
        }

        /// <summary>
        /// Удаление обработчиков контекстного меню "Копировать в" и "Переместить в"
        /// </summary>
        public void DisableCopyToMoveContextMenu()
        {
            Registry.ClassesRoot.DeleteSubKeyTree(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", false);
        }

        internal static void EnableLegacyVolumeSlider()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", "0", RegistryValueKind.DWord);
        }

        internal static void DisableLegacyVolumeSlider()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", "1", RegistryValueKind.DWord);
        }

        internal static void EnableClassicPhotoViewer()
        {
            RegistryKey regKey = Registry.ClassesRoot;

            string[] regKeys = new string[]
            {
            @"Applications\photoviewer.dll\shell\open",
            @"Applications\photoviewer.dll\shell\print",
            @"Applications\photoviewer.dll\shell\print\command",
            @"Applications\photoviewer.dll\shell\print\DropTarget",
            @"Applications\photoviewer.dll\shell\open\command",
            @"Applications\photoviewer.dll\shell\open\DropTarget",
            @"PhotoViewer.FileAssoc.Bitmap",
            @"PhotoViewer.FileAssoc.Bitmap\DefaultIcon",
            @"PhotoViewer.FileAssoc.Bitmap\shell\open\command",
            @"PhotoViewer.FileAssoc.Bitmap\shell\open\DropTarget",
            @"PhotoViewer.FileAssoc.JFIF",
            @"PhotoViewer.FileAssoc.JFIF\DefaultIcon",
            @"PhotoViewer.FileAssoc.JFIF\shell\open",
            @"PhotoViewer.FileAssoc.JFIF\shell\open\command",
            @"PhotoViewer.FileAssoc.JFIF\shell\open\DropTarget",
            @"PhotoViewer.FileAssoc.Jpeg",
            @"PhotoViewer.FileAssoc.Jpeg\DefaultIcon",
            @"PhotoViewer.FileAssoc.Jpeg\shell\open",
            @"PhotoViewer.FileAssoc.Jpeg\shell\open\command",
            @"PhotoViewer.FileAssoc.Jpeg\shell\open\DropTarget",
            @"PhotoViewer.FileAssoc.Gif",
            @"PhotoViewer.FileAssoc.Gif\DefaultIcon",
            @"PhotoViewer.FileAssoc.Gif\shell\open\command",
            @"PhotoViewer.FileAssoc.Gif\shell\open\DropTarget",
            @"PhotoViewer.FileAssoc.Png",
            @"PhotoViewer.FileAssoc.Png\DefaultIcon",
            @"PhotoViewer.FileAssoc.Png\shell\open\command",
            @"PhotoViewer.FileAssoc.Png\shell\open\DropTarget",
            @"PhotoViewer.FileAssoc.Wdp",
            @"PhotoViewer.FileAssoc.Wdp\DefaultIcon",
            @"PhotoViewer.FileAssoc.Wdp\shell\open",
            @"PhotoViewer.FileAssoc.Wdp\shell\open\command"
            };

            foreach (string keyName in regKeys)
            {
                using (RegistryKey key = regKey.CreateSubKey(keyName))
                {
                    switch (keyName)
                    {
                        case @"Applications\photoviewer.dll\shell\open":
                            key.SetValue("MuiVerb", "@photoviewer.dll,-3043");
                            break;
                        case @"Applications\photoviewer.dll\shell\open\command":
                        case @"Applications\photoviewer.dll\shell\print\command":
                            key.SetValue("", @"%SystemRoot%\System32\rundll32.exe ""%ProgramFiles%\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen %1");
                            break;
                        case @"Applications\photoviewer.dll\shell\print":
                            key.SetValue("NeverDefault", "");
                            break;
                        case @"PhotoViewer.FileAssoc.Bitmap":
                        case @"PhotoViewer.FileAssoc.JFIF":
                        case @"PhotoViewer.FileAssoc.Jpeg":
                        case @"PhotoViewer.FileAssoc.Gif":
                        case @"PhotoViewer.FileAssoc.Png":
                        case @"PhotoViewer.FileAssoc.Wdp":
                            key.SetValue("ImageOptionFlags", 0x00000001, RegistryValueKind.DWord);
                            break;
                        case @"PhotoViewer.FileAssoc.Bitmap\DefaultIcon":
                            key.SetValue("", @"%SystemRoot%\System32\imageres.dll,-70");
                            break;
                        case @"PhotoViewer.FileAssoc.JFIF\DefaultIcon":
                        case @"PhotoViewer.FileAssoc.Jpeg\DefaultIcon":
                        case @"PhotoViewer.FileAssoc.Gif\DefaultIcon":
                            key.SetValue("", @"%SystemRoot%\System32\imageres.dll,-72");
                            break;
                        case @"PhotoViewer.FileAssoc.Png\DefaultIcon":
                            key.SetValue("", @"%SystemRoot%\System32\imageres.dll,-71");
                            break;
                        case @"PhotoViewer.FileAssoc.Wdp\DefaultIcon":
                            key.SetValue("", @"%SystemRoot%\System32\wmphoto.dll,-400");
                            break;
                        case @"PhotoViewer.FileAssoc.JFIF\shell\open":
                            key.SetValue("MuiVerb", "@photoviewer.dll,-3055");
                            break;
                        case @"PhotoViewer.FileAssoc.JFIF\shell\open\command":
                        case @"PhotoViewer.FileAssoc.Jpeg\shell\open\command":
                        case @"PhotoViewer.FileAssoc.Gif\shell\open\command":
                        case @"PhotoViewer.FileAssoc.Png\shell\open\command":
                        case @"PhotoViewer.FileAssoc.Wdp\shell\open\command":
                            key.SetValue("", @"""%ProgramFiles%\Windows Photo Viewer\PhotoViewer.dll"" %1");
                            break;
                        case @"PhotoViewer.FileAssoc.JFIF\shell\open\DropTarget":
                        case @"PhotoViewer.FileAssoc.Jpeg\shell\open\DropTarget":
                        case @"PhotoViewer.FileAssoc.Gif\shell\open\DropTarget":
                        case @"PhotoViewer.FileAssoc.Png\shell\open\DropTarget":
                        case @"PhotoViewer.FileAssoc.Wdp\shell\open\DropTarget":
                            key.SetValue("Clsid", "{FFE2A43C-56B9-4bf5-9A79-CC6D4285608A}");
                            break;
                    }
                }
            }

        }

        internal static void DisableClassicPhotoViewer()
        {
            string currentUserKey = @"HKEY_CURRENT_USER";
            string localMachineKey = @"HKEY_LOCAL_MACHINE";

            try
            {
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.bmp", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.ico", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.jfif", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.jpg", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.jpeg", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.gif", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.png", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.tif", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.tiff", null, "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\.wdp", null, "", RegistryValueKind.String);

                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.bmp\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.gif\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.ico\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpeg\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpg\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jfif\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.png\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tif\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tiff\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.wdp\OpenWithProgids", "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);

                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\jpegfile\shell\open\DropTarget", "Clsid", "", RegistryValueKind.String);
                Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\pngfile\shell\open\DropTarget", "Clsid", "", RegistryValueKind.String);
                Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities\FileAssociations", false);
                Registry.ClassesRoot.DeleteSubKey(@"Applications\photoviewer.dll\shell\open", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Bitmap", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.JFIF", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Jpeg", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Gif", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Png", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Wdp", false);
                Registry.ClassesRoot.DeleteSubKeyTree(@"SystemFileAssociations\image\shell\Image Preview", false);

                Registry.SetValue(localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities", "ApplicationDescription", "@%ProgramFiles%\\Windows Photo Viewer\\photoviewer.dll,-3069", RegistryValueKind.String);
                Registry.SetValue(localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities", "ApplicationName", "@%ProgramFiles%\\Windows Photo Viewer\\photoviewer.dll,-3009", RegistryValueKind.String);

                Registry.SetValue(localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities\FileAssociations", ".tif", "PhotoViewer.FileAssoc.Tiff", RegistryValueKind.String);
                Registry.SetValue(localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities\FileAssociations", ".tiff", "PhotoViewer.FileAssoc.Tiff", RegistryValueKind.String);

                
            }
            catch (Exception ex)
            {
               
            }

        }

        internal static void EnableRGBHighlighting()
        {

            int red = 222;
            int green = 222;
            int blue = 55;

            string rgbValue = $"{red} {green} {blue}";

            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", rgbValue, RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", rgbValue, RegistryValueKind.String);

        }

        internal static void DisableRGBHighlighting()
        {

            int red = 0;
            int green = 0;
            int blue = 255;

            string rgbValue = $"{red} {green} {blue}";

            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", rgbValue, RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", rgbValue, RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", rgbValue, RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", rgbValue, RegistryValueKind.String);

        }

    }
}
