using System.Net.Http.Headers;

namespace ClientAPI.API
{
    class ClienteServer
	{
		public readonly HttpClient httpClient;
		public readonly string UriServer;
		public readonly string MediaType;

		/// <summary>
		/// Faz a conexão com o servidor
		/// </summary>
		/// <param name="UriServer">Uri do servidor. exemplo: "http://localhost:5072"</param>
		/// <param name="mediaType">É usado para indicar o tipo de mídia do corpo da mensagem HTTP</param>
		public ClienteServer(string uriServer, MediaType mediaType)
		{
			this.UriServer = uriServer;
			this.MediaType = mediaType.MediaToString();

			httpClient = new HttpClient();

			httpClient.BaseAddress = new Uri(uriServer);
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType.MediaToString()));
			
		}
		public void CloseCliente()
			=> httpClient.Dispose();
	}
}