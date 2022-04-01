namespace _3dMedia.Test.Executor.Http
{
    public class MessageHandler
    {
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }

        public MessageHandler(string errorMessage = "", object data = null, bool error = false)
        {
            this.ErrorMessage = errorMessage;
            this.Data = data ?? null;
            this.Error = error;
        }


    }
}
