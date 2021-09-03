CREATE TABLE [dbo].[RoleOperation] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [RoleId]      UNIQUEIDENTIFIER NOT NULL,
    [OperationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RoleOperation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RoleOperation_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_RoleOperation_Operations] FOREIGN KEY ([OperationId]) REFERENCES [dbo].[Operations] ([Id])
);

