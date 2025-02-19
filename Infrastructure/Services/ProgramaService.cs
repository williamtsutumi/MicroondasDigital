﻿using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class ProgramaService : IProgramaService
{
    private readonly IProgramaRepository _repository;

    public ProgramaService(IProgramaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Programa> GetProgramasPadroes()
    {
        return _repository.GetProgramasPadroes();
    }

    public void CreatePrograma(Programa programa)
    {

    }
}
