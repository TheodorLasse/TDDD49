namespace ChatApp.Model
{
    class ProtocolMessage
    {
        public ProtocolType RequestType { get; set; }
        public string Message { get; set; }

        public ProtocolMessage(ProtocolType requestType, string message)
        {
            RequestType = requestType;
            Message = message;
        }

        public ProtocolMessage() { }
    }

    enum ProtocolType
    {
        Message,
        Connect,
        Accept,
        Deny,
        Shake
    }
}
