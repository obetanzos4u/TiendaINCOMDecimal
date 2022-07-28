using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Mapeos
/// </summary>
public class Mapeos
{
    public static  MapperConfiguration getContactos = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<ModelContactosValidador, contacto>());

    public static MapperConfiguration getDireccionFacturacion = 
        new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<ModelDireccionFacturacionValidador, direcciones_facturacion>());

    public static MapperConfiguration getDireccionesEnvio =
       new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<direcciones_envio , Modeldirecciones_envio>());

}