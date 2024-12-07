This POC shows how redis can be connected to the projects.

Currently implemented dotnet 9 with EF Core.

# setup redis server on Ubuntu
```bash
apt update
apt install redis-tools # for redis-cli
snap install redis
```

open port 6379 in firewall
```bash
ufw allow 6379
```

mark server protected-mode as no
```bash
redis-cli CONFIG SET protected-mode no
```

# dotnet 9
To start the project, postgres server is needed. If there is none, you can initialize one using docker:

// If you want to run embedded in terminal session and been removed after closing
$ docker run --name postgres-redis-poc -p 5432:5432 -e POSTGRES_PASSWORD=Test123. --rm postgres

// If you want to run in background
$ docker run --name postgres-redis-poc -p 5432:5432 -e POSTGRES_PASSWORD=Test123. -d postgres

// Edit the connection string which is hardcoded in RedisPOCDbContext.cs

// Run db migration