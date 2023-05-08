using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;


namespace ClientAPI.API
{
	/// <summary>
	/// Cria a conexão com a API através de uma Rota
	/// </summary>
	/// <typeparam name="TDataType">O objeto que sera trafegado pela API, a Classe tem que corresponder ao texto da serverAPI </typeparam>
	class ConnectionAPI<TDataType> where TDataType : class
	{
		private ClienteServer clienteServer;
		private string routeApi = string.Empty;
		
		/// <summary>
		/// Representa uma relação direta com a API
		/// </summary>
		/// <param name="clienteServer">Servidor a se conectar</param>
		/// <param name="routeApi">Nome da rota para a API. Não use Barras(/)</param>
		public ConnectionAPI(ClienteServer clienteServer, string routeApi)
		{
			this.clienteServer = clienteServer; 
			this.routeApi = routeApi;
		}
		private StringContent ObjectJson(TDataType currentObject)
			=> new StringContent(JsonConvert.SerializeObject(currentObject), Encoding.UTF8, clienteServer.mediaType);

		public void CloseConnection()
			=> clienteServer.httpClient.Dispose();

		public async Task<List<TDataType>> GetAsync()
		{
			HttpResponseMessage response = await clienteServer.httpClient.GetAsync($"/{routeApi}");
			
			if (response.IsSuccessStatusCode)
			{
				var receivedAPI = await response.Content.ReadAsStringAsync();

				var produtos = JsonConvert.DeserializeObject<List<TDataType>>(receivedAPI);

				return produtos!;
			}
			else
			{
				Debug.WriteLine($"Falha na requisição HTTP: {response.StatusCode} \nUma Lista Vazia sera retornada");
				return new List<TDataType>();
			}
		}
		public async Task<TDataType> GetAsync(int id)
		{

			HttpResponseMessage response = await clienteServer.httpClient.GetAsync($"/{routeApi}/{id.ToString()}");
			
			if (response.IsSuccessStatusCode)
			{
				try
				{
					var receivedAPI = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<TDataType>(receivedAPI)!;

				}
				catch
				{
					Debug.WriteLine($"Error Index fora do intervalo");
					return null!;
				}
			}
			else
			{
				Debug.WriteLine($"Falha na requisição HTTP: {response.StatusCode}! \nNULL será retornado");
				return null!;
			}
		}
		public async Task CreateAsync(TDataType currentObject)
		{			
            var content = ObjectJson(currentObject);

			HttpResponseMessage response = await clienteServer.httpClient.PostAsync(routeApi, content);

			if(response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"StatusCode: {response.StatusCode} \nResposta da API: {responseContent}");
			}
			else
			{
				Debug.WriteLine($"Erro ao enviar a solicitação. StatusCode: {response.StatusCode}");
			}
		}
		public async Task UpdateAsync(int id, TDataType currentObject)
		{			
            var content = ObjectJson(currentObject);

			HttpResponseMessage response = await clienteServer.httpClient.PutAsync($"/{routeApi}/{id.ToString()}", content);

			if(response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"StatusCode: {response.StatusCode} \nResposta da API: {responseContent}");
			}
			else
			{
				Debug.WriteLine($"Erro ao enviar a solicitação. StatusCode: {response.StatusCode}");
			}
		}
		public async Task DeleteAsync(int id)
		{			
			HttpResponseMessage response = await clienteServer.httpClient.DeleteAsync($"{routeApi}/{id.ToString()}");

			if(response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"StatusCode: {response.StatusCode} \nResposta da API: {responseContent}");
			}
			else
			{
				Debug.WriteLine($"Erro ao enviar a solicitação. StatusCode: {response.StatusCode}");
			}
		}
	}
}