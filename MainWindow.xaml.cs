﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AddImageResolutionToFileName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class SettingValue
    {
        [XmlElement("WindowPosition")]
        public string settingWindowPositionVar = "";
        [XmlElement("ManualWidth")]
        public string settingManualWidthVar = "";
        [XmlElement("ManualHeight")]
        public string settingManualHeightVar = "";
        [XmlElement("JointSymbolFileName")]
        public string settingJointSymbolFileNameVar = "";
        [XmlElement("JointSymbolResolution")]
        public string settingJointSymbolResolutionVar = "";
        [XmlElement("FieldStatus")]
        public string settingFieldStatusVar = "";
    }

    public partial class MainWindow : Window
    {
        private SettingValue settingValue = new SettingValue();
        private string settingFilePath = Directory.GetCurrentDirectory();
        private string settingFileName = @"Setting.xml";
        private string settingSplitSymbol = ",";
        private string windowPositionVar = "";
        private string fieldStatusVar = "";
        private string resolutionModeVar = "resolutionWHButton";
        private string positionModeVar = "positionEnd";
        private string writeModeVar = "writeModeOverwrite";
        private string[] fileNames = new string[0];
        private string manualWidthVar = "";
        private string manualHeightVar = "";
        private string jointSymbolFileNameVar = "";
        private string jointSymbolResolutionVar = "";
        private bool manualWidthTextState = true;
        private bool manualHeightTextState = true;
        private bool jointSymbolFileNameTextState = true;
        private bool jointSymbolResolutionTextState = true;
        public string bindingManualWidth { get; set; } = string.Empty;
        public string bindingManualHeight { get; set; } = string.Empty;
        public string bindingJointSymbolFileName { get; set; } = string.Empty;
        public string bindingJointSymbolResolution { get; set; } = string.Empty;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Directory.SetCurrentDirectory(settingFilePath);
            if (File.Exists(settingFileName))
            {
                try
                {
                    StreamReader streamReader = new StreamReader(settingFileName, new UTF8Encoding(false));
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingValue));
                    settingValue = (SettingValue)serializer.Deserialize(streamReader)!;
                    streamReader.Close();
                    windowPositionVar = settingValue.settingWindowPositionVar != null && settingValue.settingWindowPositionVar.ToString().Length != 0 ? settingValue.settingWindowPositionVar.ToString() : "";
                    if (windowPositionVar != null && windowPositionVar.Length != 0)
                    {
                        try
                        {
                            string[] positions = windowPositionVar.Split(settingSplitSymbol);
                            double positionTop;
                            double positionLeft;
                            double positionWidth;
                            double positionHeight;
                            double.TryParse(positions[0], out positionTop);
                            double.TryParse(positions[1], out positionLeft);
                            double.TryParse(positions[2], out positionWidth);
                            double.TryParse(positions[3], out positionHeight);
                            if (positionTop < SystemParameters.VirtualScreenHeight && positionLeft < SystemParameters.VirtualScreenWidth)
                            {
                                WindowStartupLocation = WindowStartupLocation.Manual;
                                Top = positionTop;
                                Left = positionLeft;
                                Width = positionWidth;
                                Height = positionHeight;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.ToString());
                            WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        }
                    }
                    Loaded += (s, e) =>
                    {
                        manualWidth.Text = manualWidthVar = settingValue.settingManualWidthVar != null && settingValue.settingManualWidthVar.ToString().Length != 0 ? settingValue.settingManualWidthVar.ToString() : "";
                        manualHeight.Text = manualHeightVar = settingValue.settingManualHeightVar != null && settingValue.settingManualHeightVar.ToString().Length != 0 ? settingValue.settingManualHeightVar.ToString() : "";
                        jointSymbolFileName.Text = jointSymbolFileNameVar = settingValue.settingJointSymbolFileNameVar != null && settingValue.settingJointSymbolFileNameVar.ToString().Length != 0 ? settingValue.settingJointSymbolFileNameVar.ToString() : "";
                        jointSymbolResolution.Text = jointSymbolResolutionVar = settingValue.settingJointSymbolResolutionVar != null && settingValue.settingJointSymbolResolutionVar.ToString().Length != 0 ? settingValue.settingJointSymbolResolutionVar.ToString() : "";
                    };
                    fieldStatusVar = settingValue.settingFieldStatusVar != null && settingValue.settingFieldStatusVar.ToString().Length != 0 ? settingValue.settingFieldStatusVar.ToString() : "";
                    if (fieldStatusVar.Length == 22 && Regex.IsMatch(fieldStatusVar, "^([01][01],[01][01][01],[01][01],[01][01],[01],[01][01][01][01][01][01][01])$"))
                    {
                        string[] fieldStatusList = fieldStatusVar.Split(settingSplitSymbol);
                        //Tab Selected Status
                        if (fieldStatusList[0] == "10")
                        {
                            tabGeneral.IsSelected = true;
                        }
                        else if (fieldStatusList[0] == "01")
                        {
                            tabFormat.IsSelected = true;
                        }
                        else
                        {
                            tabGeneral.IsSelected = true;
                        }
                        //Resolution Mode Selected Status
                        if (fieldStatusList[1] == "100")
                        {
                            resolutionWHButton.IsChecked = true;
                            manualWidth.IsReadOnly = true;
                            manualHeight.IsReadOnly = true;
                        }
                        else if (fieldStatusList[1] == "010")
                        {
                            resolutionHWButton.IsChecked = true;
                            manualWidth.IsReadOnly = true;
                            manualHeight.IsReadOnly = true;
                        }
                        else if (fieldStatusList[1] == "001")
                        {
                            resolutionManualButton.IsChecked = true;
                            manualWidth.IsReadOnly = false;
                            manualHeight.IsReadOnly = false;
                        }
                        else
                        {
                            resolutionWHButton.IsChecked = true;
                            manualWidth.IsReadOnly = true;
                            manualHeight.IsReadOnly = true;
                        }
                        //Position Selected Status
                        if (fieldStatusList[2] == "10")
                        {
                            positionStart.IsChecked = true;
                        }
                        else if (fieldStatusList[2] == "01")
                        {
                            positionEnd.IsChecked = true;
                        }
                        else
                        {
                            positionEnd.IsChecked = true;
                        }
                        //Write Mode Selected Status
                        if (fieldStatusList[3] == "10")
                        {
                            writeModeCopy.IsChecked = true;
                        }
                        else if (fieldStatusList[3] == "01")
                        {
                            writeModeOverwrite.IsChecked = true;
                        }
                        else
                        {
                            writeModeOverwrite.IsChecked = true;
                        }
                        //Tree Expanded Status
                        if (fieldStatusList[4] == "1")
                        {
                            formatImage.IsExpanded = true;
                        }
                        else
                        {
                            formatImage.IsExpanded = false;
                        }
                        //Format CheckBox Selected Status
                        format_png.IsChecked = fieldStatusList[5].Substring(0, 1) == "1" ? true : false;
                        format_jpg.IsChecked = fieldStatusList[5].Substring(1, 1) == "1" ? true : false;
                        format_ico.IsChecked = fieldStatusList[5].Substring(2, 1) == "1" ? true : false;
                        format_bmp.IsChecked = fieldStatusList[5].Substring(3, 1) == "1" ? true : false;
                        format_tiff.IsChecked = fieldStatusList[5].Substring(4, 1) == "1" ? true : false;
                        format_gif.IsChecked = fieldStatusList[5].Substring(5, 1) == "1" ? true : false;
                        format_webp.IsChecked = fieldStatusList[5].Substring(6, 1) == "1" ? true : false;
                    }
                    else
                    {
                        tabGeneral.IsSelected = true;
                        resolutionWHButton.IsChecked = true;
                        positionEnd.IsChecked = true;
                        writeModeOverwrite.IsChecked = true;
                        manualWidth.IsReadOnly = true;
                        manualHeight.IsReadOnly = true;
                        formatImage.IsExpanded = true;
                        format_png.IsChecked = true;
                        format_jpg.IsChecked = true;
                        format_ico.IsChecked = true;
                        format_bmp.IsChecked = true;
                        format_tiff.IsChecked = true;
                        format_gif.IsChecked = true;
                        format_webp.IsChecked = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }
            else
            {
                tabGeneral.IsSelected = true;
                resolutionWHButton.IsChecked = true;
                positionEnd.IsChecked = true;
                writeModeOverwrite.IsChecked = true;
                manualWidth.IsReadOnly = true;
                manualHeight.IsReadOnly = true;
                formatImage.IsExpanded = true;
                //formatVideo.IsExpanded = true;
                format_png.IsChecked = true;
                format_jpg.IsChecked = true;
                format_ico.IsChecked = true;
                format_bmp.IsChecked = true;
                format_tiff.IsChecked = true;
                format_gif.IsChecked = true;
                format_webp.IsChecked = true;
            }
        }

        private void manualWidthTextChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(manualWidth.Text.ToString(), "([\\\\\\/\\:\\*\\?\\\"\\>\\<\\|])"))
            {
                manualWidthVar = manualWidth.Text.ToString();
                manualWidthTextState = true;
                if (manualWidthTextState && manualHeightTextState && jointSymbolFileNameTextState && jointSymbolResolutionTextState)
                {
                    stateField.Text = "Drop in this window";
                    stateField.Foreground = new SolidColorBrush(Colors.Black);
                    stateField.FontSize = 22;
                }
            }
            else
            {
                stateField.Text = "Can't use \\ / : * ? \" > < |";
                stateField.Foreground = new SolidColorBrush(Colors.Red);
                stateField.FontSize = 17;
                manualWidthTextState = false;
            }
        }

        private void manualHeightTextChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(manualHeight.Text.ToString(), "([\\\\\\/\\:\\*\\?\\\"\\>\\<\\|])"))
            {
                manualHeightVar = manualHeight.Text.ToString();
                manualHeightTextState = true;
                if (manualWidthTextState && manualHeightTextState && jointSymbolFileNameTextState && jointSymbolResolutionTextState)
                {
                    stateField.Text = "Drop in this window";
                    stateField.Foreground = new SolidColorBrush(Colors.Black);
                    stateField.FontSize = 22;
                }
            }
            else
            {
                stateField.Text = "Can't use \\ / : * ? \" > < |";
                stateField.Foreground = new SolidColorBrush(Colors.Red);
                stateField.FontSize = 17;
                manualHeightTextState = false;
            }
        }

        private void jointSymbolFileNameTextChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(jointSymbolFileName.Text.ToString(), "([\\\\\\/\\:\\*\\?\\\"\\>\\<\\|])"))
            {
                jointSymbolFileNameVar = jointSymbolFileName.Text.ToString();
                jointSymbolFileNameTextState = true;
                if (manualWidthTextState && manualHeightTextState && jointSymbolFileNameTextState && jointSymbolResolutionTextState)
                {
                    stateField.Text = "Drop in this window";
                    stateField.Foreground = new SolidColorBrush(Colors.Black);
                    stateField.FontSize = 22;
                }
            }
            else
            {
                stateField.Text = "Can't use \\ / : * ? \" > < |";
                stateField.Foreground = new SolidColorBrush(Colors.Red);
                stateField.FontSize = 17;
                jointSymbolFileNameTextState = false;
            }
        }

        private void jointSymbolResolutionTextChangedEventHandler(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(jointSymbolResolution.Text.ToString(), "([\\\\\\/\\:\\*\\?\\\"\\>\\<\\|])"))
            {
                jointSymbolResolutionVar = jointSymbolResolution.Text.ToString();
                jointSymbolResolutionTextState = true;
                if (manualWidthTextState && manualHeightTextState && jointSymbolFileNameTextState && jointSymbolResolutionTextState)
                {
                    stateField.Text = "Drop in this window";
                    stateField.Foreground = new SolidColorBrush(Colors.Black);
                    stateField.FontSize = 22;
                }
            }
            else
            {
                stateField.Text = "Can't use \\ / : * ? \" > < |";
                stateField.Foreground = new SolidColorBrush(Colors.Red);
                stateField.FontSize = 17;
                jointSymbolResolutionTextState = false;
            }
        }

        private void resolutionRadioButton(object sender, RoutedEventArgs e)
        {
            if (sender == resolutionWHButton)
            {
                resolutionModeVar = "resolutionWHButton";
                manualWidth.IsReadOnly = true;
                manualHeight.IsReadOnly = true;
            }
            else if (sender == resolutionHWButton)
            {
                resolutionModeVar = "resolutionHWButton";
                manualWidth.IsReadOnly = true;
                manualHeight.IsReadOnly = true;
            }
            else if (sender == resolutionManualButton)
            {
                resolutionModeVar = "resolutionManualButton";
                manualWidth.IsReadOnly = false;
                manualHeight.IsReadOnly = false;
            }
            Debug.Print(resolutionModeVar);
        }

        private void positionRadioButton(object sender, RoutedEventArgs e)
        {
            if (sender == positionStart)
            {
                positionModeVar = "positionStart";
            }
            else if (sender == positionEnd)
            {
                positionModeVar = "positionEnd";
            }
            Debug.Print(positionModeVar);
        }

        private void writeModeRadioButton(object sender, RoutedEventArgs e)
        {
            if (sender == writeModeCopy)
            {
                writeModeVar = "writeModeCopy";
            }
            else if (sender == writeModeOverwrite)
            {
                writeModeVar = "writeModeOverwrite";
            }
            Debug.Print(writeModeVar);
        }

        private void fileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string fileName in fileNames)
                {
                    if (File.GetAttributes(fileName).HasFlag(FileAttributes.Directory))
                    {
                        Debug.Print("フォルダー");
                        searchFolder(fileName);
                    }
                    else
                    {
                        Debug.Print("ファイル");
                        changeImageFileNameProcess(fileName);
                    }
                }
            }
        }

        private void fileDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && manualWidthTextState && manualHeightTextState && jointSymbolFileNameTextState && jointSymbolResolutionTextState)
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void changeImageFileNameProcess(string inputFile)
        {
            string fileExtension = System.IO.Path.GetExtension(inputFile);
            string originalFileName = System.IO.Path.GetFileNameWithoutExtension(inputFile);
            string fileDirectory = System.IO.Path.GetDirectoryName(inputFile) + @"\";
            string pixelWidth = "";
            string pixelHeight = "";
            string newFileName = "";
            if (Regex.IsMatch(fileExtension, "(png|jpg|jpeg|jpe|jfif|jfi|jif|pjpeg|pjp|ico|bmp|dib|tiff|tif|gif|webp)", RegexOptions.IgnoreCase))
            {
                try
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.UriSource = new Uri(inputFile);
                    image.EndInit();
                    image.Freeze();
                    pixelWidth = image.PixelWidth.ToString();
                    pixelHeight = image.PixelHeight.ToString();
                    Debug.Print(pixelWidth);
                    Debug.Print(pixelHeight);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
                if (Regex.IsMatch(fileExtension, "png", RegexOptions.IgnoreCase) && format_png.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
                else if (Regex.IsMatch(fileExtension, "(jpg|jpeg|jpe|jfif|jfi|jif|pjpeg|pjp)", RegexOptions.IgnoreCase) && format_jpg.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
                else if (Regex.IsMatch(fileExtension, "ico", RegexOptions.IgnoreCase) && format_ico.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
                else if (Regex.IsMatch(fileExtension, "(bmp|dib)", RegexOptions.IgnoreCase) && format_bmp.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
                else if (Regex.IsMatch(fileExtension, "(tiff|tif)", RegexOptions.IgnoreCase) && format_tiff.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
                else if (Regex.IsMatch(fileExtension, "gif", RegexOptions.IgnoreCase) && format_gif.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
                else if (Regex.IsMatch(fileExtension, "webp", RegexOptions.IgnoreCase) && format_webp.IsChecked == true)
                {
                    newFileName = makeNewMediaFileName(resolutionModeVar, positionModeVar, originalFileName, jointSymbolFileNameVar, jointSymbolResolutionVar, pixelWidth, pixelHeight, manualWidthVar, manualHeightVar);
                    writeFileName(writeModeVar, inputFile, fileDirectory + newFileName + fileExtension);
                }
            }
            if (Regex.IsMatch(fileExtension, "mp4", RegexOptions.IgnoreCase))
            {
                Debug.Print(fileExtension);
            }
            else if (Regex.IsMatch(fileExtension, "mov", RegexOptions.IgnoreCase))
            {
                Debug.Print(fileExtension);
            }
            else if (Regex.IsMatch(fileExtension, "avi", RegexOptions.IgnoreCase))
            {
                Debug.Print(fileExtension);
            }
            else
            {
                Debug.Print("other");
            }
        }

        private void searchFolder(string path)
        {
            string[] files = Directory.GetFiles(path, "*", System.IO.SearchOption.AllDirectories);
            foreach (string file in files)
            {
                changeImageFileNameProcess(file);
            }
        }

        private string makeNewMediaFileName(string resolutionMode, string position, string fileName, string jointFileName, string jointResolution, string imageAutoWidth, string imageAutoHeight, string imageManualWidth, string imageManualHeight)
        {
            string newName = "";
            string tmpName = "";
            string tmpWidth = "";
            string tmpHeight = "";
            if (resolutionMode == "resolutionWHButton" || resolutionMode == "resolutionHWButton")
            {
                tmpWidth = imageAutoWidth;
                tmpHeight = imageAutoHeight;
            }
            else if (resolutionMode == "resolutionManualButton")
            {
                tmpWidth = imageManualWidth;
                tmpHeight = imageManualHeight;
            }
            if (resolutionMode == "resolutionWHButton" || resolutionMode == "resolutionManualButton")
            {
                tmpName = tmpWidth + jointResolution + tmpHeight;
            }
            else if (resolutionMode == "resolutionHWButton")
            {
                tmpName = tmpHeight + jointResolution + tmpWidth;
            }
            if (position == "positionStart")
            {
                newName = tmpName + jointFileName + fileName;
            }
            else if (position == "positionEnd")
            {
                newName = fileName + jointFileName + tmpName;
            }
            return newName;
        }

        private void writeFileName(string mode, string input, string output)
        {
            try
            {
                Debug.Print("WirteMode:" + mode + " InputFile:" + input + " OutputFile:" + output);
                if (mode == "writeModeCopy")
                {
                    File.Copy(input, output, true);
                }
                else if (mode == "writeModeOverwrite")
                {
                    File.Move(input, output);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        private void windowCloseing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            settingValue.settingWindowPositionVar = Top.ToString() + settingSplitSymbol + Left.ToString() + settingSplitSymbol + Width.ToString() + settingSplitSymbol + Height.ToString();
            settingValue.settingManualWidthVar = manualWidthVar;
            settingValue.settingManualHeightVar = manualHeightVar;
            settingValue.settingJointSymbolFileNameVar = jointSymbolFileNameVar;
            settingValue.settingJointSymbolResolutionVar = jointSymbolResolutionVar;
            string tmp = "";
            if (tabGeneral.IsSelected == true)
            {
                tmp = tmp + "10" + settingSplitSymbol;
            }
            else if (tabFormat.IsSelected == true)
            {
                tmp = tmp + "01" + settingSplitSymbol;
            }
            else
            {
                tmp = tmp + "10" + settingSplitSymbol;
            }
            if (resolutionWHButton.IsChecked == true)
            {
                tmp = tmp + "100" + settingSplitSymbol;
            }
            else if (resolutionHWButton.IsChecked == true)
            {
                tmp = tmp + "010" + settingSplitSymbol;
            }
            else if (resolutionManualButton.IsChecked == true)
            {
                tmp = tmp + "001" + settingSplitSymbol;
            }
            else
            {
                tmp = tmp + "100" + settingSplitSymbol;
            }
            if (positionStart.IsChecked == true)
            {
                tmp = tmp + "10" + settingSplitSymbol;
            }
            else if (positionEnd.IsChecked == true)
            {
                tmp = tmp + "01" + settingSplitSymbol;
            }
            else
            {
                tmp = tmp + "10" + settingSplitSymbol;
            }
            if (writeModeCopy.IsChecked == true)
            {
                tmp = tmp + "10" + settingSplitSymbol;
            }
            else if (writeModeOverwrite.IsChecked == true)
            {
                tmp = tmp + "01" + settingSplitSymbol;
            }
            else
            {
                tmp = tmp + "10" + settingSplitSymbol;
            }
            if (formatImage.IsExpanded == true)
            {
                tmp = tmp + "1" + settingSplitSymbol;
            }
            else
            {
                tmp = tmp + "0" + settingSplitSymbol;
            }
            tmp = tmp + (format_png.IsChecked == true ? "1" : "0");
            tmp = tmp + (format_jpg.IsChecked == true ? "1" : "0");
            tmp = tmp + (format_ico.IsChecked == true ? "1" : "0");
            tmp = tmp + (format_bmp.IsChecked == true ? "1" : "0");
            tmp = tmp + (format_tiff.IsChecked == true ? "1" : "0");
            tmp = tmp + (format_gif.IsChecked == true ? "1" : "0");
            tmp = tmp + (format_webp.IsChecked == true ? "1" : "0");
            settingValue.settingFieldStatusVar = tmp;
            try
            {
                Directory.SetCurrentDirectory(settingFilePath);
                XmlSerializer serializer = new XmlSerializer(typeof(SettingValue));
                StreamWriter streamWriter = new StreamWriter(settingFileName, false, new UTF8Encoding(false));
                serializer.Serialize(streamWriter, settingValue);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }
    }

    public class FileNameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.Print("input");
            if (!Regex.IsMatch((string)value, "([\\\\\\/\\:\\*\\?\\\"\\>\\<\\|])"))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, null);
            }
        }
    }
}
