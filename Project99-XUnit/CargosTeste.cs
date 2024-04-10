
using Moq;
using Project99.Model;
using Project99.Negocio;

namespace Project99_XUnit
{
    public class CargosTeste
    {
        Mock<ICargoRepository> _repositoryMock;
        Negocio _negocio;

        List<Cargo>cargosEsperados = new ()
        {
            new Cargo { Id = 1, Nome = "Gerente", Descricao = "teste1" },
            new Cargo { Id = 2, Nome = "Desenvolvedor", Descricao = "teste2" }
        };

        public CargosTeste()
        {
            _repositoryMock = new Mock<ICargoRepository>();

            // Configurar o mock para retornar uma tarefa concluída com a lista de cargos esperados
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(cargosEsperados);

            _negocio = new Negocio(_repositoryMock.Object);
        }

        [Fact(DisplayName = "ListCargo_ReturnsListOfCargo")]
        public async Task ListCargo_ReturnsListOfCargo()
        {
            // Act
            var resultado = await _negocio.ListarCargos();

            // Assert

            Assert.Equal(cargosEsperados, resultado);

        }

        [Theory(DisplayName = "GetCargo_ReturnsListOfCargo")]
        [InlineData(1)]
        public async Task GetCargo_ReturnsListOfCargo(int id)
        {
            // Act
            var resultado = await _negocio.BuscarCargos(id);

            // Assert
            resultado.Id.Should().Be(id);

        }
    }
}