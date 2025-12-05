# Balzor WBS

Application Blazor Server permettant de créer et manipuler un WBS (Work Breakdown Structure) avec envoi simulé vers un WebService.

## Fonctionnalités

- Création, chargement et suppression de projets (stubs de WebService en mémoire).
- Affichage hiérarchique indenté des tâches avec colonnes Charge, Consommé, RAF, Consommation %, Avance/Retard % et date de livraison.
- Calculs agrégés automatiques par niveau et synthèse de projet.
- Edition des tâches les plus basses, ajout, suppression, déplacement, indentation/désindentation.

## Démarrage

Le projet est un Blazor Server ciblant .NET 8. Il repose sur des stubs en mémoire ; aucune API réelle n'est requise. Lancez l'application avec `dotnet run` dans le dossier `BalzorWbs` une fois le SDK .NET installé.
