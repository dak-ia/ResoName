using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ResoName
{
    public class ProcessingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double processingFilesNumNowVar = 0;
        private double processingFilesNumAllVar = 0;
        private double processingRatioVar = 0;
        private string processingMessageVar = "Processing";
        private string processingStatusMessageVar = "0/0   0%";
        private string processingFileNameVar = "";

        public double processingFilesNumNow
        {
            get { return processingFilesNumNowVar; }
            set
            {
                processingFilesNumNowVar = value;
                OnPropertyChanged(nameof(processingFilesNumNow));
            }
        }
        public double processingFilesNumAll
        {
            get { return processingFilesNumAllVar; }
            set
            {
                processingFilesNumAllVar = value;
                OnPropertyChanged(nameof(processingFilesNumAll));
            }
        }
        public double processingRatio
        {
            get { return processingRatioVar; }
            set
            {
                processingRatioVar = value;
                OnPropertyChanged(nameof(processingRatio));
            }
        }
        public string processingMessage
        {
            get { return processingMessageVar; }
            set
            {
                processingMessageVar = value;
                OnPropertyChanged(nameof(processingMessage));
            }
        }
        public string processingStatusMessage
        {
            get { return processingStatusMessageVar; }
            set
            {
                processingStatusMessageVar = value;
                OnPropertyChanged(nameof(processingStatusMessage));
            }
        }
        public string processingFileName
        {
            get { return processingFileNameVar; }
            set
            {
                processingFileNameVar = value;
                OnPropertyChanged(nameof(processingFileName));
            }
        }

        public void resetValue()
        {
            processingFilesNumNowVar = 0;
            processingFilesNumAllVar = 0;
            processingRatioVar = 0;
            processingMessageVar = "Processing";
            processingStatusMessageVar = "0/0   0%";
            processingFileNameVar = "";
        }

        public void setProcessingMessage(double ratio, bool status = true, string message = "")
        {
            if (!status)
            {
                processingMessageVar = message;
            }
            else if (ratio >= 100)
            {
                processingMessageVar = "Complete";
            }
            else if (processingMessage == "Processing")
            {
                processingMessageVar = "Processing.";
            }
            else if (processingMessage == "Processing.")
            {
                processingMessageVar = "Processing..";
            }
            else if (processingMessage == "Processing..")
            {
                processingMessageVar = "Processing...";
            }
            else
            {
                processingMessageVar = "Processing";
            }
            OnPropertyChanged(nameof(processingMessage));
        }

        public void setFilesStatus(int finished, int total)
        {
            processingFilesNumNowVar += finished;
            processingFilesNumAllVar += total;
            processingRatioVar = Math.Floor((processingFilesNumNow / processingFilesNumAll) * 100);
            processingStatusMessageVar = $"{processingFilesNumNow}/{processingFilesNumAll}   {processingRatioVar}%";
            OnPropertyChanged(nameof(processingFilesNumNow));
            OnPropertyChanged(nameof(processingFilesNumAll));
            OnPropertyChanged(nameof(processingRatio));
            OnPropertyChanged(nameof(processingStatusMessage));
            setProcessingMessage(processingRatio);
        }

        public void setFileName(string fileName)
        {
            processingFileNameVar = fileName;
            OnPropertyChanged(nameof(processingFileName));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}