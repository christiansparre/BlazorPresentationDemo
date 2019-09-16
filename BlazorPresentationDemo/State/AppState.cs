using System;

namespace BlazorPresentationDemo.State
{
    public class AppState
    {
        private string globalMessage;

        public string GlobalMessage
        {
            get => globalMessage;
            set
            {
                if (value != globalMessage)
                {
                    GlobalMessageChanged?.Invoke();
                }
                globalMessage = value;
            }
        }


        public event Action GlobalMessageChanged;
    }
}
