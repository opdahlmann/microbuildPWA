using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MicroBuild.Management.Domain.ServiceClients
{
    public class EmailClient : EmailApiClient
    {
        public EmailClient(string userId) : base(userId) { }
        public EmailClient() : base() { }


        public async Task<bool> SendMail(string projectId, ByteArrayContent byteContent)
        {
            try
            {
                var httpResponse = await this.HttpClient.PostAsync("email/sendnotification", byteContent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendMail(ByteArrayContent byteContent)
        {
            try
            {
                var httpResponse = await this.HttpClient.PostAsync("email/sendnotification", byteContent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendMailForDoorMessages(string projectId, ByteArrayContent byteContent)
        {
            try
            {
                var httpResponse = await this.HttpClient.PostAsync("email/sendmessagemails", byteContent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SendMailForIssueMessages(string projectId, ByteArrayContent byteContent)
        {
            try
            {
                var httpResponse = await this.HttpClient.PostAsync("email/sendissuemessagemails", byteContent);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}