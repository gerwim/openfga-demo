# OpenFGA demo

## Getting started
1. Run `docker compose up -d`
2. Open [http://localhost:3000/playground](http://localhost:3000/playground)
3. Create a store
4. Add the model:
```
model
  schema 1.1

type user
  relations
    define can_edit: hr from organization
    define organization: [organization]

type organization
  relations
    define employee: [user]
    define hr: [department#member]

type department
  relations
    define member: [user]
```