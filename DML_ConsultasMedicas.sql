USE ConsultasMedicasDB;
GO

/* Inserindo dados para as colunas das tabelas abaixo*/
/* No qual o m�dico sempre ser� com o Id 1, e o paciente, com o Id 2. */
INSERT INTO TipoUsuario(Tipo) VALUES ('M�dico');
INSERT INTO TipoUsuario(Tipo) VALUES ('Paciente');

INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Hagrid', 'hagrid@email.com', '123456', 1);
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Snape', 'snape@email.com', '678910', 1);
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Minerva', 'minerva@email.com', '1102938', 1);
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Harry', 'harry@email.com', '147258', 2);
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Hermione', 'hermione@email.com', '9876543', 2);
INSERT INTO Usuarios(Nome, Email, Senha, IdTipoUsuario) VALUES ('Rony', 'rony@email.com', '6549873', 2);

INSERT INTO Especialidade(Categoria) VALUES ('Cardiologista');
INSERT INTO Especialidade(Categoria) VALUES ('Cl�nico Geral');
INSERT INTO Especialidade(Categoria) VALUES ('Dermatologista');
INSERT INTO Especialidade(Categoria) VALUES ('Endocrinologista');
INSERT INTO Especialidade(Categoria) VALUES ('Otorrinolaringologista');

INSERT INTO Medico(CRM, IdEspecialidade, IdUsuario) VALUES ('651466213', 1, 3);
INSERT INTO Medico(CRM, IdEspecialidade, IdUsuario) VALUES ('651466213', 5, 1);
INSERT INTO Medico(CRM, IdEspecialidade, IdUsuario) VALUES ('651466213', 2, 2);

INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('684656356', '1980-05-08T12:50:36', 1, 4 );
INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('91452641561', '1983-02-23T05:14:01', 1, 5 );
INSERT INTO Paciente(Carteirinha, DataNascimento, Ativo, IdUsuario) VALUES ('46514951209', '1985-05-1T10:03:52', 1, 6 );


INSERT INTO Consultas(DataHora, IdMedico, IdPaciente) VALUES (GETDATE(), 1, 1);
INSERT INTO Consultas(DataHora, IdMedico, IdPaciente) VALUES ('2022-11-08T12:00:00', 2, 3);
INSERT INTO Consultas(DataHora, IdMedico, IdPaciente) VALUES ('2022-10-15T12:00:00', 3, 4);



