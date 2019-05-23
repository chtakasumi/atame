using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class OrgaoExpeditor
    {
        public string Sigla { get; private set; }
        public string Descricao { get; private set; }
        private readonly List<OrgaoExpeditor> _orgaosExpeditores;

        public OrgaoExpeditor(string sigla, string descricao)
        {
            Sigla = sigla;
            Descricao = descricao;
        }

        public OrgaoExpeditor()
        {

            this._orgaosExpeditores = new List<OrgaoExpeditor> {


                new OrgaoExpeditor("ABNC","Academia Brasileira de Neurocirurgia"),
                new OrgaoExpeditor("CGPI/DUREX/DPF","Coordenação Geral de Polícia de Imigração da Polícia Federal"),
                new OrgaoExpeditor("CGPI","Coordenação-Geral de Privilégios e Imunidades"),
                new OrgaoExpeditor("CGPMAF","Coordenadoria Geral de Polícia Marítima, Aeronáutica e de Fronteiras"),
                new OrgaoExpeditor("CNIG","Conselho Nacional de Imigração"),
                new OrgaoExpeditor("CNT","Carteira Nacional de Habilitação"),
                new OrgaoExpeditor("COREN","Conselho Regional de Enfermagem"),
                new OrgaoExpeditor("CORECON","Conselho Regional de Economia"),
                new OrgaoExpeditor("CRA","Conselho Regional de Administração"),
                new OrgaoExpeditor("CRAS","Centro de Referência de Assistência Social"),
                new OrgaoExpeditor("CRB","Conselho Regional de Biblioteconomia"),
                new OrgaoExpeditor("CRC","Conselho Regional de Contabilidade"),
                new OrgaoExpeditor("CRE","Conselho Regional de Estatística"),
                new OrgaoExpeditor("CREA","Conselho Regional de Engenharia e Agronomia"),
                new OrgaoExpeditor("CRECI","Conselho Regional de Corretores de Imóveis"),
                new OrgaoExpeditor("CREFIT","Conselho Regional de Fisioterapia e Terapia Ocupacional"),
                new OrgaoExpeditor("CRESS","Conselho Regional de Serviço Social"),
                new OrgaoExpeditor("CRF","Conselho Regional de Farmácia"),
                new OrgaoExpeditor("CRM","Conselho Regional de Medicina"),
                new OrgaoExpeditor("CRN","Conselho Regional de Nutrição"),
                new OrgaoExpeditor("CRO","Conselho Regional de Odontologia"),
                new OrgaoExpeditor("CRP","Conselho Regional de Psicologia"),
                new OrgaoExpeditor("CRPRE","Conselho Regional de Profissionais de Relações Públicas"),
                new OrgaoExpeditor("CRQ","Conselho Regional de Química"),
                new OrgaoExpeditor("CRRC","Conselho Regional de Representantes Comerciais"),
                new OrgaoExpeditor("CRMV","Conselho Regional de Medicina Veterinária"),
                new OrgaoExpeditor("CSC","Carteira Sede Carpina de Pernambuco"),
                new OrgaoExpeditor("CTPS","Carteira de Trabalho e Previdência Social"),
                new OrgaoExpeditor("DIC","Diretoria de Identificação Civil"),
                new OrgaoExpeditor("DIREX","Diretoria-Executiva"),
                new OrgaoExpeditor("DPMAF","Divisão de Polícia Marítima, Área e de Fronteiras"),
                new OrgaoExpeditor("DPT","Departamento de Polícia Técnica Geral"),
                new OrgaoExpeditor("DST","Programa Municipal DST/Aids"),
                new OrgaoExpeditor("FGTS","Fundo de Garantia do Tempo de Serviço"),
                new OrgaoExpeditor("FIPE","Fundação Instituto de Pesquisas Econômicas"),
                new OrgaoExpeditor("FLS","Fundação Lyndolpho Silva"),
                new OrgaoExpeditor("GOVGO","Governo do Estado de Goiás"),
                new OrgaoExpeditor("I CLA","Carteira de Identidade Classista"),
                new OrgaoExpeditor("IFP","Instituto Félix Pacheco"),
                new OrgaoExpeditor("IGP","Instituto Geral de Perícias"),
                new OrgaoExpeditor("IICCECF / RO","Instituto de Identificação Civil e Criminal Engrácia da Costa Francisco de Rondônia"),
                new OrgaoExpeditor("IIMG","Inter-institutional Monitoring Group"),
                new OrgaoExpeditor("IML","Instituto Médico - Legal"),
                new OrgaoExpeditor("IPC","Índice de Preços ao Consumidor"),
                new OrgaoExpeditor("IPF","Instituto Pereira Faustino"),
                new OrgaoExpeditor("MAE","Ministério da Aeronáutica"),
                new OrgaoExpeditor("MEX","Ministério do Exército"),
                new OrgaoExpeditor("MMA","Ministério da Marinha"),
                new OrgaoExpeditor("OAB","Ordem dos Advogados do Brasil"),
                new OrgaoExpeditor("OMB","Ordens dos Músicos do Brasil"),
                new OrgaoExpeditor("PCMG","Policia Civil do Estado de Minas Gerais"),
                new OrgaoExpeditor("PMMG","Polícia Militar do Estado de Minas Gerais"),
                new OrgaoExpeditor("POF/DPF","Polícia Federal"),
                new OrgaoExpeditor("POM","Polícia Militar"),
                new OrgaoExpeditor("SSP","Secretaria de Segurança Pública"),
                new OrgaoExpeditor("SDS","Secretaria de Defesa Social(Pernambuco)"),
                new OrgaoExpeditor("SNJ","Secretaria Nacional de Justiça / Departamento de Estrangeiros"),
                new OrgaoExpeditor("SECC","Secretaria de Estado da Casa Civil"),
                new OrgaoExpeditor("SEJUSP","Secretaria de Estado de Justiça e Segurança Pública – Mato Grosso"),
                new OrgaoExpeditor("SES/EST","Carteira de Estrangeiro"),
                new OrgaoExpeditor("SESP","Secretaria de Estado da Segurança Pública do Paraná"),
                new OrgaoExpeditor("SJS","Secretaria da Justiça e Segurança"),
                new OrgaoExpeditor("SJTC","Secretaria da Justiça do Trabalho e Cidadania"),
                new OrgaoExpeditor("SJTS","Secretaria da Justiça do Trabalho e Segurança"),
                new OrgaoExpeditor("SPTC","Secretaria de Polícia Técnico - Científica"),
               
            };
        }

        public List<OrgaoExpeditor> GetAll()
        {
            return this._orgaosExpeditores;
        }
    }
}
