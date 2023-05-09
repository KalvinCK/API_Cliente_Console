using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;


namespace ClientAPI.API
{
	/// <summary>
	/// Cria a conexão com a API através de uma Rota
	/// </summary>
	/// <typeparam name="TClassType">O objeto que sera trafegado pela API, a Classe tem que corresponder ao texto da serverAPI </typeparam>
	class RouteAPI<TClassType> where TClassType : class
	{
		private ClienteServer clienteServer;
		private string routeController = string.Empty;
		
		/// <summary>
		/// Representa uma relação direta com a API
		/// </summary>
		/// <param name="clienteServer">Servidor a se conectar</param>
		/// <param name="routeController">Nome da rota para a API. Não use Barras(/)</param>
		public RouteAPI(ClienteServer clienteServer, string routeController)
		{
			this.clienteServer = clienteServer; 
			this.routeController = routeController;
		}
		private StringContent ObjectJson(TClassType currentObject)
			=> new StringContent(JsonConvert.SerializeObject(currentObject), Encoding.UTF8, clienteServer.MediaType);

		public async Task<List<TClassType>> GetAsync()
		{
			HttpResponseMessage response = await clienteServer.httpClient.GetAsync($"/{routeController}");
			
			if (response.IsSuccessStatusCode)
			{
				var receivedAPI = await response.Content.ReadAsStringAsync();

				var produtos = JsonConvert.DeserializeObject<List<TClassType>>(receivedAPI);

				return produtos!;
			}
			else
			{
				Console.WriteLine($"Falha na requisição HTTP: {response.StatusCode} \nUma Lista Vazia sera retornada");
				return new List<TClassType>();
			}
		}
		public async Task<TClassType> GetAsync(int id)
		{

			HttpResponseMessage response = await clienteServer.httpClient.GetAsync($"/{routeController}/{id.ToString()}");
			
			if (response.IsSuccessStatusCode)
			{
				try
				{
					var receivedAPI = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<TClassType>(receivedAPI)!;

				}
				catch
				{
					Console.WriteLine($"Error:: A tabela não foi encontrada, O ID pode estar fora do intervalo");
					return Activator.CreateInstance<TClassType>();
				}
			}
			else
			{
				Console.WriteLine($"Falha na requisição HTTP: {response.StatusCode}! \nNULL será retornado");
				return Activator.CreateInstance<TClassType>();
			}
		}
		public async Task CreateAsync(TClassType currentObject)
		{			
            var content = ObjectJson(currentObject);

			HttpResponseMessage response = await clienteServer.httpClient.PostAsync(routeController, content);

			if(response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"StatusCode: {response.StatusCode} \nResposta da API: {responseContent}");
			}
			else
			{
				Console.WriteLine($"Erro ao enviar a solicitação. StatusCode: {response.StatusCode}");
			}
		}
		public async Task UpdateAsync(int id, TClassType currentObject)
		{			
            var content = ObjectJson(currentObject);

			HttpResponseMessage response = await clienteServer.httpClient.PutAsync($"/{routeController}/{id.ToString()}", content);

			if(response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"StatusCode: {response.StatusCode} \nResposta da API: {responseContent}");
			}
			else
			{
				Console.WriteLine($"Erro ao enviar a solicitação. StatusCode: {response.StatusCode}");
			}
		}
		public async Task DeleteAsync(int id)
		{			
			HttpResponseMessage response = await clienteServer.httpClient.DeleteAsync($"{routeController}/{id.ToString()}");

			if(response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"StatusCode: {response.StatusCode} \nResposta da API: {responseContent}");
			}
			else
			{
				Console.WriteLine($"Erro ao enviar a solicitação. StatusCode: {response.StatusCode}");
			}
		}
	}
}