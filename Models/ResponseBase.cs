namespace NetworkManagerApi.Models
{
    public abstract class ResponseBase<T>
    {
        virtual public bool HasError { get; }
        virtual public string Message { get; }
        virtual public T Data { get; }

        public static ResponseBase<A> CreateSuccess<A>(A data) => new Success<A>(data);

        public static ResponseBase<A> CreateFailure<A>(string message) => new Failure<A>(message);

        private class Success<A> : ResponseBase<A>
        {

            private readonly A _data;

            public Success(A data) => _data = data;

            override
            public bool HasError => false;

            override
            public string Message => "";

            override
            public A Data => _data;
        }

        private class Failure<A> : ResponseBase<A>
        {
            private readonly string _message;
            public Failure(string message) => _message = message;

            override
            public bool HasError => true;

            override
            public string Message => _message;

            override
            public A Data => default;
        }
    }
}