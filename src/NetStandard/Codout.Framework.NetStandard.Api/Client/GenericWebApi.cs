﻿using Codout.Framework.NetStandard.Api.Dto;
using Codout.Framework.NetStandard.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Codout.Framework.NetStandard.Api.Client
{

    /// <summary>
    /// Classe genérica para operações CRUD em WebAPI
    /// </summary>
    /// <typeparam name="T">Tipo do objeto a ser tratado</typeparam>
    /// <typeparam name="TId">Tipo do Id do objeto</typeparam>
    public class GenericWebApi<T, TId> : IWebApi<T, TId> where T : DtoBase<TId>
    {
        private readonly HttpClient _client;
        private readonly string _uriService;

        public GenericWebApi(string uriService, string baseUrl)
        {
            _uriService = uriService;
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Retorma um IEnumerable do objeto tipado
        /// </summary>
        /// <returns>IEnumerable</returns>
        public async Task<IEnumerable<T>> Get()
        {
            IEnumerable<T> itens = null;

            var response = await _client.GetAsync(_uriService);

            if (response.IsSuccessStatusCode)
            {
                itens = await response.Content.ReadAsAsync<IEnumerable<T>>();
            }

            return itens;
        }

        /// <summary>
        /// Retorna um objeto de acordo com o id
        /// </summary>
        /// <param name="id">Id do objeto</param>
        /// <returns>Objeto</returns>
        public async Task<T> Get(TId id)
        {
            T obj = null;

            var response = await _client.GetAsync($"{_uriService}/{id}");

            if (response.IsSuccessStatusCode)
            {
                obj = await response.Content.ReadAsAsync<T>();
            }

            return obj;
        }

        /// <summary>
        /// Recebe e retorna o objeto tipado
        /// </summary>
        /// <param name="obj">Objeto a ser tratado</param>
        /// <returns>Objeto</returns>
        public async Task<T> Post(T obj)
        {
            var response = await _client.PostAsJsonAsync($"{_uriService}", obj);

            response.EnsureSuccessStatusCode();

            obj = await response.Content.ReadAsAsync<T>();

            return obj;
        }

        /// <summary>
        /// Recebe e retorna o objeto tipado
        /// </summary>
        /// <param name="obj">Objeto a ser tratado</param>
        /// <returns>Objeto</returns>
        public async Task<T> Put(T obj)
        {
            var response = await _client.PostAsJsonAsync($"{_uriService}/{obj.Id}", obj);

            response.EnsureSuccessStatusCode();

            obj = await response.Content.ReadAsAsync<T>();

            return obj;
        }

        /// <summary>
        /// Deleta o objeto identitificado pelo id
        /// </summary>
        /// <param name="id">Id do objeto</param>
        /// <returns>StatusCode da operação</returns>
        public async Task<HttpStatusCode> Delete(TId id)
        {
            var response = await _client.DeleteAsync($"{_uriService}/{id}");
            return response.StatusCode;
        }
    }
}
