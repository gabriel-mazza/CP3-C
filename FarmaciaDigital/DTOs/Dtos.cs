namespace FarmaciaDigital.DTOs
{
   
    public class CriarFilialDTO
    {
        public required string Nome { get; set; }
        public required string Endereco { get; set; }
        public required string Cidade { get; set; }
    }

    public class FilialResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
    }

    public class CriarPessoaFisicaDTO
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int FilialId { get; set; }
    }

    
    public class CriarPessoaJuridicaDTO
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string Cnpj { get; set; }
        public required string RazaoSocial { get; set; }
        public int FilialId { get; set; }
    }

    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public int FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;
    }

    
    public class CriarPlanoSaudeDTO
    {
        public required string Nome { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public required string Cobertura { get; set; }
        public decimal MensalidadeBase { get; set; }
    }

    public class ProdutoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;
        public decimal MensalidadeBase { get; set; }
    }

    
    public class CriarAdesaoDTO
    {
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
    }

    public class AdesaoResponseDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal? MensalidadeEfetiva { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataAdesao { get; set; }
    }
}
