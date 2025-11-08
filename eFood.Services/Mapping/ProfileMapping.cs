using AutoMapper;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Mapping
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {

            CreateMap<Database.Dojmovi, Model.Dojmovi>();
            CreateMap<DojmoviSearchObject, Database.Dojmovi>();
            CreateMap<DojmoviInsertRequest, Database.Dojmovi>();
            CreateMap<DojmoviUpsertRequest, Database.Dojmovi>();


            CreateMap<Database.KorisniciUloge, Model.KorisniciUloge>();
            CreateMap<KorisniciUlogaSearchRequest, Database.KorisniciUloge>();
            CreateMap<KorisniciUlogeInsertRequest, Database.KorisniciUloge>();
            CreateMap<KorisniciUlogeUpdateRequest, Database.KorisniciUloge>();

            CreateMap<Database.Drzava, Model.Drzava>();
            CreateMap<DojmoviSearchObject, Database.Dojmovi>();

            CreateMap<Database.Korisnici, Model.Korisnik>().ReverseMap();

            CreateMap<KorisnikSearchRequests, Database.Korisnici>();
            CreateMap<KorisnikInsertRequest, Database.Korisnici>();
            CreateMap<KorisnikUpsertRequest, Database.Korisnici>();

            CreateMap<Database.KorisniciUloge, Model.KorisniciUloge>();
            CreateMap<Database.Uloge, Model.Uloge>();

            CreateMap<Database.Grad, Model.Grad>();
            CreateMap<GradSearchObject, Database.Grad>();

            CreateMap<Database.Restoran, Model.Restoran>();
            CreateMap<RestoranInsertRequest, Database.Restoran>();
            CreateMap<RestoranUpdateRequest, Database.Restoran>();


            CreateMap<Database.Prilozi, Model.Prilozi>();
            CreateMap<PriloziSearchObject, Database.Prilozi>();

            CreateMap<Database.Jelo, Model.Jelo>();
            CreateMap<JeloSearchObject, Database.Jelo>();
            CreateMap<JeloUpsertRequest, Database.Jelo>();
            CreateMap<JeloUpsertRequest, Database.Jelo>();

            CreateMap<Database.Kategorija, Model.Kategorija>();
            CreateMap<KategorijaSearchRequest, Database.Kategorija>();
            CreateMap<KategorijaInsertRequest, Database.Kategorija>();
            CreateMap<KategorijaUpdateRequest, Database.Kategorija>();

            CreateMap<Services.Database.Narudzba, Model.Narudzba>()
    .ForMember(d => d.StatusNarudzbe,
               opt => opt.MapFrom(s => s.StatusNarudzbe != null ? s.StatusNarudzbe.Naziv : null));

            CreateMap<NarudzbaSearchObject, Database.Narudzba>();
            CreateMap<NarudzbaInsertRequest, Database.Narudzba>();
            CreateMap<NarudzbaUpdateRequest, Database.Narudzba>();

            CreateMap<Database.Narudzba, Model.Narudzba>()
            .ForMember(d => d.StatusNarudzbe,
                opt => opt.MapFrom(s => s.StatusNarudzbe != null ? s.StatusNarudzbe.Naziv : null));

            CreateMap<Model.Requests.NarudzbaInsertRequest, Database.Narudzba>()
                .ForMember(d => d.StatusNarudzbe, opt => opt.Ignore()); // <-- BITNO

            CreateMap<Model.Requests.StavkaInsertRequest, Database.StavkeNarudzbe>();

            CreateMap<Database.Status, Model.StatusNarudzbe>();
            CreateMap<StatusNarudzbeSearchObject, Database.Status>();

            CreateMap<Database.StavkeNarudzbe, Model.StavkeNarudzbe>();
            CreateMap<StavkeNarudzbeSearchObject, Services.Database.StavkeNarudzbe>();
            CreateMap<StavkeNarudzbeSearchObject, Services.Database.StavkeNarudzbe>();
            CreateMap<StavkeNarudzbeUpdateRequest, Services.Database.StavkeNarudzbe>();

            CreateMap<Database.Uplata, Model.Uplata>();
            CreateMap<UplataSearchObject, Database.Uplata>();
            CreateMap<UplataInsertRequest, Database.Uplata>();
            CreateMap<UplateUpdateRequest, Database.Uplata>();

            CreateMap<Database.Korpa, Model.Korpa>();
            CreateMap<KorpaSearchObject, Database.Korpa>();
            CreateMap<KorpaInsertRequest, Database.Korpa>();
            CreateMap<KorpaUpdateRequest, Database.Korpa>();





        }
    } 
}
