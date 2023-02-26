
To add migration:

	Add-Migration -Name MigrationName -Project DroneManager.Infrastructure -Context DbContextWrite -StartupProject DroneManager.Server -Verbose

	dotnet ef --project ../DroneManager.Infrastructure --startup-project ./ migrations add "MigrationName" --context DbContextWrite

To update database:

	Update-Database -Project DroneManager.Infrastructure -Context DbContextWrite -StartupProject DroneManager.Server -Verbose

	dotnet ef --project ../DroneManager.Infrastructure --startup-project ./ database update --context DbContextWrite

To remove a migration (not runned):

	Remove-Migration -Project DroneManager.Infrastructure -Context DbContextWrite -StartupProject DroneManager.Server -Verbose

	dotnet ef --project ../DroneManager.Infrastructure --startup-project ./ migrations remove --context DbContextWrite

To undo an already runned execute migration follow this steps:

	1. Update the Database to the previous migration:

		Update-Database -Migration MigrationName -Project DroneManager.Infrastructure -Context DbContextWrite -StartupProject DroneManager.Server -Verbose
	
		dotnet ef --project ../DroneManager.Infrastructure --startup-project ./ database update MigrationName --context DbContextWrite

	2. Remove the latest migrations:
		
		Remove-Migration -Project DroneManager.Infrastructure -Context DbContextWrite -StartupProject DroneManager.Server -Verbose

		dotnet ef --project ../DroneManager.Infrastructure --startup-project ./ migrations remove --context DbContextWrite