using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using lojaGames.Util;
using Microsoft.EntityFrameworkCore;

namespace lojaGames.Model
{
    public class Produto
    {
        [Key] // pRIMARY kEY (Id)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // IDENTITY(1,1)
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Nome { get; set ;} = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Descricao {get; set;} = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Console {get; set;} = string.Empty;

        [Column(TypeName = "date")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime DataLancamento { get; set; }

        [Column(TypeName = "decimal")]
        [Precision(7,2)]
        public decimal Preco { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string Foto { get; set; } = string.Empty;

        public virtual Categoria? Categoria { get; set; }

    }
}