using System.Net.Http.Headers;

namespace ClientAPI.API
{
    class ClienteServer
	{
		public HttpClient httpClient { get; }
		public readonly string UriServer;
		public readonly string mediaType;

		/// <summary>
		/// Faz a conexão com o servidor
		/// </summary>
		/// <param name="UriServer">Uri do servidor. exemplo: "http://localhost:5072"</param>
		/// <param name="mediaType">É usado para indicar o tipo de mídia do corpo da mensagem HTTP</param>
		public ClienteServer(string UriServer, MediaType mediaType)
		{
			this.UriServer = UriServer;
			this.mediaType = mediaType.MediaToString();

			httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri(UriServer);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType.MediaToString()));
			
		}
		public void CloseCliente()
			=> httpClient.Dispose();
	}
}