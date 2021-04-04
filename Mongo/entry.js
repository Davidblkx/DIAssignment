db.createUser({
  user: 'mongo',
  pwd: 'mongo',
  roles: [
    {
      role: 'readWrite',
      db: 'events',
    },
    {
      role: 'readWrite',
      db: 'projections',
    },
  ],
});