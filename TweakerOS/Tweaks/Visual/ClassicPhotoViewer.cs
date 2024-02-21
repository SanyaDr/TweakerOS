using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Visual;

/// <summary>
/// Открыть фото через классический фото редактор Windows
/// </summary>
//TODO Check working
public class ClassicPhotoViewer : ITweak
{
    public string Name => "Открывать фотографии через классическое приложение Windows";
    public string Description => "Открывать фотографии через классическое приложение Windows";

    public bool GetIsChanged()
    {
        return true;
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
    {
        RegistryKey regKey = Registry.ClassesRoot;

        string[] regKeys =
        [
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
        ];

        foreach (string keyName in regKeys)
        {
            using RegistryKey key = regKey.CreateSubKey(keyName);
            switch (keyName)
            {
                case @"Applications\photoviewer.dll\shell\open":
                    key.SetValue("MuiVerb", "@photoviewer.dll,-3043");
                    break;
                case @"Applications\photoviewer.dll\shell\open\command":
                case @"Applications\photoviewer.dll\shell\print\command":
                    key.SetValue("",
                        @"%SystemRoot%\System32\rundll32.exe ""%ProgramFiles%\Windows Photo Viewer\PhotoViewer.dll"", ImageView_Fullscreen %1");
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

    public void Disable()
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

            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.bmp\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.gif\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.ico\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpeg\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpg\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jfif\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.png\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tif\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.tiff\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);
            Registry.SetValue(
                currentUserKey + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.wdp\OpenWithProgids",
                "PhotoViewer.FileAssoc.Tiff", "", RegistryValueKind.String);

            Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\jpegfile\shell\open\DropTarget", "Clsid", "",
                RegistryValueKind.String);
            Registry.SetValue(currentUserKey + @"\SOFTWARE\Classes\pngfile\shell\open\DropTarget", "Clsid", "",
                RegistryValueKind.String);
            Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities\FileAssociations",
                false);
            Registry.ClassesRoot.DeleteSubKey(@"Applications\photoviewer.dll\shell\open", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Bitmap", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.JFIF", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Jpeg", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Gif", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Png", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"PhotoViewer.FileAssoc.Wdp", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"SystemFileAssociations\image\shell\Image Preview", false);

            Registry.SetValue(localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities",
                "ApplicationDescription", "@%ProgramFiles%\\Windows Photo Viewer\\photoviewer.dll,-3069",
                RegistryValueKind.String);
            Registry.SetValue(localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities",
                "ApplicationName", "@%ProgramFiles%\\Windows Photo Viewer\\photoviewer.dll,-3009",
                RegistryValueKind.String);

            Registry.SetValue(
                localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities\FileAssociations", ".tif",
                "PhotoViewer.FileAssoc.Tiff", RegistryValueKind.String);
            Registry.SetValue(
                localMachineKey + @"\SOFTWARE\Microsoft\Windows Photo Viewer\Capabilities\FileAssociations", ".tiff",
                "PhotoViewer.FileAssoc.Tiff", RegistryValueKind.String);
        }
        catch (Exception ex)
        {
            // TODO обработка ошибок
        }
    }
}