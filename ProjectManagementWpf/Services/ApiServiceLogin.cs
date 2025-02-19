using ProjectManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementWpf.Services
{
    public class ApiServiceLogin
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7208/api/Doljnosti")
        };

        public async Task<DoljnostiDto> AuthenticateAsync(string login, string password)
        {
            try
            {
                var loginDto = new { Post = login, Password = password };

                Console.WriteLine($"Отправка запроса: Post={loginDto.Post}, Password={loginDto.Password}");

                // Адрес должен совпадать с маршрутом в контроллере
                var response = await _httpClient.PostAsJsonAsync("Doljnosti/authenticate", loginDto);
                Console.WriteLine($"Статус ответа: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var userRole = await response.Content.ReadFromJsonAsync<DoljnostiDto>();

                    if (userRole != null)
                    {
                        Console.WriteLine($"Пользователь найден: {userRole.Post}");
                        return userRole;
                    }
                }
                else
                {
                    Console.WriteLine($"Ошибка аутентификации: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return null;
        }

    }

}