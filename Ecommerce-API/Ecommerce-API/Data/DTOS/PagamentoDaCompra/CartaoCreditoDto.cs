﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.FinalizacaoCompra
{
    public class CartaoCreditoDto
    {
        [Required]
        public string CPF { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "O número está incorreto.")]
        public string NumeroCartao { get; set; }

        [StringLength(3, ErrorMessage = "O número está incorreto.")]
        [Required]
        public string CVV { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        [Required]
        public double Parcelamento { get; set; }
    }
}
