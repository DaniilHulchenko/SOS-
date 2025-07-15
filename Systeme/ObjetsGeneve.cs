using System;

public class TarifDivers
{
	public float TarifRapportInterne = 0;
	public float TarifBonAdmission = 0;
	public float TarifPetitRapport = 0;
	public float TarifMoyenRapport = 0;
	public float TarifGrandRapport = 0;
	public float TarifPoliceJour = 0;
	public float TarifPoliceNuit = 0;
	public TarifDivers(){}
}

public class ActeGeneve
{
	public int IdActe = -1;
	public string LibelleActe="";
	public float Tarif=0;

	public ActeGeneve()
	{
	}
	public ActeGeneve(int id,string libelle , float tarif)
	{
		this.IdActe = id;
		this.LibelleActe = libelle;
		this.Tarif = tarif;
	}
}

public class Assurance_SS
{
	public int IdService = -1;
	public string LibelleService="";
	
	public Assurance_SS()
	{
	}
	public Assurance_SS(int id,string libelle)
	{
		this.IdService = id;
		this.LibelleService = libelle;
	}
}

public class MaterielGeneve
{
	public int IdMateriel = -1;
	public string LibelleMateriel="";
	public string ReferenceMateriel="";
	public float Tarif=0;
	
	public MaterielGeneve()
	{
	}
	public MaterielGeneve(int id,string libelle,string reference,float tarif)
	{
		this.IdMateriel = id;
		this.LibelleMateriel = libelle;
		this.ReferenceMateriel = reference;
		this.Tarif = tarif;
	}
}

public class AssuranceGeneve
{
	public int IdAssurance = -1;
	public string LibelleAssurance="";
	
	public AssuranceGeneve()
	{
	}
	public AssuranceGeneve(int id,string libelle)
	{
		this.IdAssurance = id;
		this.LibelleAssurance = libelle;
	}
}

public class MedicamentGeneve
{
	public int IdMedicament = -1;
	public string Reference="";
	public string Libelle="";
	public float Tarif = 0;
	
	public MedicamentGeneve()
	{
	}
	public MedicamentGeneve(int id,string libelle,string reference,float tarif)
	{
		this.IdMedicament = id;
		this.Libelle = libelle;
		this.Reference  =reference;
		this.Tarif = tarif;
	}
	public static MedicamentGeneve GetMedicament(MedicamentGeneve[] tableau,int IdMedicament)
	{
		foreach(MedicamentGeneve medic in tableau)
			if(medic.IdMedicament == IdMedicament)
				return medic;
		return null;
	}
}

public class Destinataire
{
	public enum ModeEnvoi {Fax,Mail,Aucun,Courrier};
	public enum TypeDestinataire
    {
        MedecinTraitant = 0,
        Hopital = 1,
        Interne = 2,
        Patient = 3,
        HotelPolice = 4
    };

	public Destinataire.ModeEnvoi mode;
	public Destinataire.TypeDestinataire type;
	public int Code;
	public string Nom;
	
	
	public Destinataire(int code,string nom)
	{
		
		this.Code = code;
		this.Nom = nom;
		
	}		
}
