namespace Api
{
    public static class Routes
    {
        private const string _base = "/api";

        public static class Auth
        {
            private const string _controllerBase = $"{_base}/auth";

            public const string Register = $"{_controllerBase}/register";

            public const string Login = $"{_controllerBase}/login";

            public const string RefreshToken = $"{_controllerBase}/refresh-token";

        }
        public static class Boards
        {
            private const string _controllerBase = $"{_base}/boards";

            public const string Create = $"{_controllerBase}";

            public const string GetAll = $"{_controllerBase}";

            public const string Get = $"{_controllerBase}/{{id}}";

        }
    }
}
