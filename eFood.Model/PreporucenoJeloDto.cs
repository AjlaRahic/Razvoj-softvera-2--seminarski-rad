using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model
{
    public class PreporucenoJeloDto
    {
        public Jelo Jelo { get; set; } = default!;

        public string ReasonText { get; set; } = "";
        public string ReasonCode { get; set; } = "SIMILAR_USER";
        public double Similarity { get; set; }

        public int? SimilarUserId { get; set; }
        public string? SimilarUserName { get; set; }
    }
}
