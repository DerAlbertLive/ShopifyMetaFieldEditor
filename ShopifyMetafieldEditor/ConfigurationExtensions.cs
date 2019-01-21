using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace ShopifyMetaFieldEditor
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddAzureKeyVaultWithManagedServiceIdentity(
            this IConfigurationBuilder builder, string vault)
        {
            var tokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));
            return builder.AddAzureKeyVault(vault, 
                keyVaultClient,
                new DefaultKeyVaultSecretManager());
        }
    }
}