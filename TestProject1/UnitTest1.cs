using WpfApp1;

namespace TestProject1
{
    public class Tests
    {
        

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LogInCodeTest()
        {
            //Arrange
            Boolean expected_result = true, actual_result;
            String ID = "5866927", PASSWORD_ = "1245Decarie";

            actual_result = LogInCode.LogI(ID, PASSWORD_);

            Assert.AreEqual(expected_result, actual_result);    

            Assert.That(actual_result, Is.TypeOf<Boolean>());

            Assert.Pass();
        }
    }
}