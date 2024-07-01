using System;
using System.Collections.Generic;

namespace ProductosAPI.Models;

public partial class Imagene
{
    public int Id { get; set; }

    public int IdArticulo { get; set; }

    public string ImagenUrl { get; set; } = null!;
}
