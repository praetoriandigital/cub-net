using NUnit.Framework;
using System.Net;

namespace Cub.Tests
{
    public class ProductTests
    {
        [SetUp]
        public void SetUp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        [Test]
        public void Get_Product_ShouldReturnOne()
        {
            var product = Product.Get("prd_S1NTfyEzmFueE28Q");

            Assert.NotNull(product);
            Assert.IsNotEmpty(product.Name);
            Assert.AreEqual("service", product.Type);
        }
    }
}
