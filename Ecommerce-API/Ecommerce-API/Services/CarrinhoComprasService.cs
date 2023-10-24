using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CarrinhoDeCompras;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services.Interfaces;
using System.Text.Json;

namespace Ecommerce_API.Services;

public class CarrinhoComprasService : ICarrinhoComprasService
{
    IMapper _mapper;
    ICarrinhoComprasRepository _repository;
    IProdutoRepository _produtoRepository;
    EcommerceContext _context;

    public CarrinhoComprasService(IMapper mapper, ICarrinhoComprasRepository repository,
        IProdutoRepository produtoRepository, EcommerceContext context)
    {
        _mapper = mapper;
        _repository = repository;
        _produtoRepository = produtoRepository;
        _context = context;
    }

    public async Task<Endereco> ConsultarViaCep(string cep)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var jsonObject = JsonSerializer.Deserialize<Endereco>(response);
            return jsonObject;
        }

        catch
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.ErrorMessages.cepIncorreto);
        }


    }

    public async Task<ReadCarrinhoDto> CriarCarrinho(CreateCarrinhoDto carrinhoDto)
    {
        VerificarDados(carrinhoDto.ProdutoId, carrinhoDto.Quantidade);

        var carrinho = _mapper.Map<CarrinhoDeComprasModel>(carrinhoDto);
        await _repository.CriarCarrinho(carrinho);

        var produto = BuscarProdutoPorId(carrinhoDto.ProdutoId);

        ProdutoNoCarrinhoModel produtoNoCarrinho = new ProdutoNoCarrinhoModel()
        { CarrinhoId = carrinho.Id, ProdutoId = produto.Id };

        _mapper.Map(carrinhoDto, produtoNoCarrinho);
        _mapper.Map(produto, produtoNoCarrinho);

        produtoNoCarrinho.Quantidade = carrinhoDto.Quantidade;

        var valorTotal = await CalcularValorTotal(produtoNoCarrinho.ValorProduto, produtoNoCarrinho.Quantidade);
        produtoNoCarrinho.ValorTotal = valorTotal;

        await _repository.AdicionarProdutoCarrinho(produtoNoCarrinho);
        
        return RetornoDeInformacoesCarrinho(produtoNoCarrinho);
    }

   
    public async Task<ReadCarrinhoDto> AdicionarProdutoCarrinho(AddProdutoNoCarrinhoDto carrinhoDto, int carrinhoId, 
        int produtoId)
    {
        var buscarCarrinho = _repository.BuscarCarrinhoPorId(carrinhoId);

        var produtoNocarrinho = new ProdutoNoCarrinhoModel();

        produtoNocarrinho.CarrinhoId = buscarCarrinho.Id;
        produtoNocarrinho.Quantidade = carrinhoDto.Quantidade;
                
        VerificarDados(produtoId, produtoNocarrinho.Quantidade);

        var produto = BuscarProdutoPorId(produtoId);
        produtoNocarrinho.ProdutoId = produtoId;

        _mapper.Map(produto, produtoNocarrinho);

        produtoNocarrinho.Quantidade = carrinhoDto.Quantidade;
        var valorTotal = await CalcularValorTotal(produtoNocarrinho.ValorProduto, produtoNocarrinho.Quantidade);

        produtoNocarrinho.ValorTotal = valorTotal;

        await _repository.AdicionarProdutoCarrinho(produtoNocarrinho);
        
        return RetornoDeInformacoesCarrinho(produtoNocarrinho);

    }

    public async Task<ReadCarrinhoDto> AlterarProdutoNoCarrinho(UpdateProdutoNoCarrinhoDto carrinhoDto, int carrinhoId,
        int produtoId)
    {
        var buscarCarrinho = _repository.BuscarCarrinhoPorId(carrinhoId);

        var produtoNoCarrinho = new ProdutoNoCarrinhoModel();

        produtoNoCarrinho.CarrinhoId = buscarCarrinho.Id;
        produtoNoCarrinho.Quantidade = carrinhoDto.Quantidade;

        var produto = BuscarProdutoPorId(produtoId);
        produtoNoCarrinho.ProdutoId = produtoId;

        VerificarDados(produtoId, produtoNoCarrinho.Quantidade);

        if (!_repository.BuscarProdutoECarrinho(produtoId, produtoNoCarrinho.CarrinhoId))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.ErrorMessages.produtoNaoEncontrado);
        }
    
        if(produtoNoCarrinho.Quantidade == 0)
        {
            await _repository.RemoverProdutoDoCarrinho(produtoNoCarrinho);
            return RetornoDeInformacoesCarrinho(produtoNoCarrinho);
        }
               
        
        _mapper.Map(produto, produtoNoCarrinho);

        produtoNoCarrinho.Quantidade = carrinhoDto.Quantidade;
        var valorTotal = await CalcularValorTotal(produtoNoCarrinho.ValorProduto, carrinhoDto.Quantidade);
        produtoNoCarrinho.ValorTotal = valorTotal;

        await _repository.AlterarProdutoNoCarrinho(produtoNoCarrinho);
        
        return RetornoDeInformacoesCarrinho(produtoNoCarrinho);
    }

    public async Task<ReadCarrinhoDto> AdicionarEndereçoNoCarrinho(int id, AddEndereçoNoCarrinhoDto endereçoDto)
    {
        var carrinho = _repository.BuscarCarrinhoPorId(id);

        if (carrinho == null)
        {
            throw new NameExceptions("O carrinho não foi encontrado");
        }

        var viaCep = await ConsultarViaCep(endereçoDto.CEP);
                
        _mapper.Map(viaCep, carrinho);

        carrinho.CEP = endereçoDto.CEP;
        carrinho.Numero = endereçoDto.Numero;
        carrinho.Complemento = endereçoDto.Complemento;

        await _repository.AdicionarEndereçoNoCarrinho(carrinho);

        var produtoNoCarrinho = _mapper.Map<ProdutoNoCarrinhoModel>(carrinho);
        produtoNoCarrinho.CarrinhoId = carrinho.Id;

        return RetornoDeInformacoesCarrinho(produtoNoCarrinho);

    }

    public async Task<ReadCarrinhoDto> RemoverProdutoDoCarrinho(int carrinhoId, int produtoId)
    {
        var produtoNoCarrinho = new ProdutoNoCarrinhoModel();

        if (!_repository.BuscarProdutoECarrinho(produtoId, carrinhoId))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.ErrorMessages.produtoNaoEncontrado);
        }

        produtoNoCarrinho = _repository.BuscarProdutoNoCarrinhoPorId(produtoId, carrinhoId);
        await _repository.RemoverProdutoDoCarrinho(produtoNoCarrinho);

        return RetornoDeInformacoesCarrinho(produtoNoCarrinho);

    }

    private ReadCarrinhoDto RetornoDeInformacoesCarrinho(ProdutoNoCarrinhoModel produtoNocarrinho)
    {
        List<ReadProdutoNoCarrinhoDto> produtoNoCarrinhoLista = new List<ReadProdutoNoCarrinhoDto>();

        CarrinhoDeComprasModel carrinho = new CarrinhoDeComprasModel();        

        carrinho = _repository.BuscarCarrinhoPorId(produtoNocarrinho.CarrinhoId); 
        
        var carrinhoLista = carrinho.ProdutoNoCarrinho.ToList();

        double valorTotalCarrinho = 0;

        foreach (var p in carrinhoLista)
        {
            ReadProdutoNoCarrinhoDto readProdutoDto = new ReadProdutoNoCarrinhoDto();
            _mapper.Map(p, readProdutoDto);
            produtoNoCarrinhoLista.Add(readProdutoDto);
            valorTotalCarrinho += readProdutoDto.ValorTotal;
        }

        ReadCarrinhoDto readCarrinhoDto = new ReadCarrinhoDto();

        _mapper.Map(carrinho, readCarrinhoDto);

        if (readCarrinhoDto.CEP == null)
        {
            readCarrinhoDto.CEP = " - ";
            readCarrinhoDto.Logradouro = " - ";
            readCarrinhoDto.Complemento = " - ";
            readCarrinhoDto.Bairro = " - ";
            readCarrinhoDto.Localidade = " - ";
            readCarrinhoDto.UF = " - ";

        }

        readCarrinhoDto.ProdutoNoCarrinhoLista = produtoNoCarrinhoLista;
        readCarrinhoDto.ValorTotalCarrinho = valorTotalCarrinho;
        
        return readCarrinhoDto;
    }

    private Produto? BuscarProdutoPorId(int id)
    {
        return _produtoRepository.BuscarPorId(id);
    }


    private void VerificarDados(int id, int estoque)
    {
        if (_produtoRepository.ListaProdutos().Any(produto => produto.Id == id && produto.Status == false))
        
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.ErrorMessages.produtoInativo);
        }

        if(_produtoRepository.ListaProdutos().Any(produto => produto.Id == id && produto.Quantidade < estoque))
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.ErrorMessages.produtoSemEstoque);
        }
    }

    private async Task<double> CalcularValorTotal(double valorProduto, int quantidade)
    {
        var valorTotal = valorProduto * quantidade;
        return valorTotal;
    }

    public async Task<ReadCarrinhoDto> PesquisarCarrinhoId(int id)
    {
        var carrinho = await _repository.PesquisarCarrinhoId(id);
        var produtoNocarrinho = _mapper.Map<ProdutoNoCarrinhoModel>(carrinho);
                  
        
        return RetornoDeInformacoesCarrinho(produtoNocarrinho);
    }

}
