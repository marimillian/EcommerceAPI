using AutoMapper;
using Ecommerce_API.Data.DTOS.FinalizacaoCompra;
using Ecommerce_API.Data.DTOS.PagamentoDaCompra;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Service.ValidacaoCPF;
using Ecommerce_API.Services.Interfaces;


namespace Ecommerce_API.Services;

public class PagamentoCompraService : IPagamentoCompraService
{
    ICarrinhoComprasRepository _repository;
    IProdutoRepository _produtoRepository;
    IMapper _mapper;

    public PagamentoCompraService(ICarrinhoComprasRepository repository, IMapper mapper, IProdutoRepository produtoRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _produtoRepository = produtoRepository;
    }

    public async Task<ReadCartaoCredito> PagamentoCartãoDeCredito(int carrinhoId, CartaoCreditoDto cartaoCreditoDto)
    {
        var carrinho = _repository.BuscarCarrinhoPorId(carrinhoId);
        VerificarDados(cartaoCreditoDto.CPF, cartaoCreditoDto.DataVencimento);

        if (cartaoCreditoDto.Parcelamento > 12)
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.PagamentoDeCompras.ErrorMessage.numeroParcelas);
        }

        var cartaoCredito = _mapper.Map<ReadCartaoCredito>(cartaoCreditoDto);
        _mapper.Map(carrinho, cartaoCredito);

        double valorTotalCarrinho = CalculoValorTotalCarrinho(carrinho);

        if (cartaoCreditoDto.Parcelamento >= 7)
        {
            double taxa = Ecommerce_API.ConstMessages.PagamentoDeCompras.Taxa.taxa;
            double valorTotalComJuros = await CalculoJuros(valorTotalCarrinho, taxa, cartaoCreditoDto.Parcelamento);
            double valorParcela = valorTotalComJuros / cartaoCreditoDto.Parcelamento;
            cartaoCredito.Parcelamento = $"{cartaoCredito.Parcelamento}X com juros de (3% a.m.) {valorParcela.ToString("C")}";
            cartaoCredito.ValorTotal = valorTotalComJuros.ToString("C");
            return cartaoCredito;
        }

        double ValorTotalParcela = valorTotalCarrinho / cartaoCreditoDto.Parcelamento;


        cartaoCredito.ValorTotal = valorTotalCarrinho.ToString("C");


        cartaoCredito.Parcelamento = $"{cartaoCredito.Parcelamento}X sem juros de {ValorTotalParcela.ToString("C")}";

        await AlterarEstoqueDeProdutos(carrinho);

        return cartaoCredito;
    }

 

    public async Task<ReadCartaoDebito> PagamentoCartaoDeDebito (int carrinhoId, CartaoDebitoDto cartaoDebitoDto)
    {
        var carrinho = _repository.BuscarCarrinhoPorId(carrinhoId);
        VerificarDados(cartaoDebitoDto.CPF, cartaoDebitoDto.DataVencimento);

        var cartaoDebito = _mapper.Map<ReadCartaoDebito>(cartaoDebitoDto);
        _mapper.Map(carrinho, cartaoDebito);

        double valorTotalCarrinho = CalculoValorTotalCarrinho(carrinho);

        cartaoDebito.ValorTotal = valorTotalCarrinho.ToString("C");

        await AlterarEstoqueDeProdutos(carrinho);

        return cartaoDebito;

    }

    public async Task<ReadPix> PagamentoPix (int carrinhoId)
    {
        var carrinho = _repository.BuscarCarrinhoPorId(carrinhoId);
        var pix = _mapper.Map<ReadPix>(carrinho);

        double valorTotalCarrinho = CalculoValorTotalCarrinho(carrinho);
            
        Guid codigoDoPix = Guid.NewGuid();

        double desconto = 3.0 / 100.0;
        double valorComDesconto = valorTotalCarrinho - (desconto * valorTotalCarrinho);

        pix.ValorTotal = valorComDesconto.ToString("C");
        pix.ChavePix = codigoDoPix;

        await AlterarEstoqueDeProdutos(carrinho);

        return pix;

    }

    private async Task AlterarEstoqueDeProdutos(CarrinhoDeComprasModel carrinho)
    {
        var carrinhoLista = carrinho.ProdutoNoCarrinho.ToList();

        foreach (var p in carrinhoLista)
        {
            var produto = _produtoRepository.BuscarPorId(p.ProdutoId);
            await _produtoRepository.AlterarQuantidadeDeEstoque(produto.Id, p.Quantidade, produto.Quantidade);

        }
    }

    private static double CalculoValorTotalCarrinho(CarrinhoDeComprasModel carrinho)
    {
        var carrinhoLista = carrinho.ProdutoNoCarrinho.ToList();

        double valorTotalCarrinho = 0;

        foreach (var p in carrinhoLista)
        {
            valorTotalCarrinho += p.ValorTotal;
        }

        return valorTotalCarrinho;
    }

    private void VerificarDados (string cpf, DateTime dataVencimento)
    {
        if (ValidacaoCPF.IsCpf(cpf) == false)
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.PagamentoDeCompras.ErrorMessage.cpfIncorreto);
        }

        if(dataVencimento < DateTime.Today)
        {
            throw new NameExceptions(Ecommerce_API.ConstMessages.PagamentoDeCompras.ErrorMessage.cartaoVencido);
        }

       
    }

    private async Task<double> CalculoJuros (double valorTotalParcela, double taxa, double parcela)
    {
        taxa = Ecommerce_API.ConstMessages.PagamentoDeCompras.Taxa.taxa;
        double montante = valorTotalParcela * Math.Pow((1+taxa), parcela);

        return montante;
    }
    

}
