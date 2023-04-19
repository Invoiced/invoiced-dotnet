using Invoiced;
using Xunit;

namespace InvoicedTest
{
    public class CommonUtilTest
    {
        [Fact]
        public void TestParseLinks()
        {
            var link =
                "<https://api.invoiced.com/customers?page=3&per_page=10>; rel=\"self\", <https://api.invoiced.com/customers?page=1&per_page=10>; rel=\"first\",<https://api.invoiced.com/customers?page=2&per_page=10>; rel=\"previous\",<https://api.invoiced.com/customers?page=4&per_page=10>; rel=\"next\",<https://api.invoiced.com/customers?page=5&per_page=10>; rel=\"last\"";

            var dict = CommonUtil.ParseLinks(link);

            var self = dict["self"];
            var first = dict["first"];
            var previous = dict["previous"];


            Assert.True(self == "https://api.invoiced.com/customers?page=3&per_page=10");
            Assert.True(first == "https://api.invoiced.com/customers?page=1&per_page=10");
            Assert.True(previous == "https://api.invoiced.com/customers?page=2&per_page=10");
        }
    }
}