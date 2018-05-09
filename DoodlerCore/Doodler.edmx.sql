
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2018 17:46:16
-- Generated from EDMX file: D:\Projects\Doodler\DoodlerCore\Doodler.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Doodler];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Inbox_Id] int  NOT NULL
);
GO

-- Creating table 'Polls'
CREATE TABLE [dbo].[Polls] (
    [Id] uniqueidentifier  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [EndsAt] datetime  NOT NULL,
    [Creator_Id] int  NOT NULL
);
GO

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [Id] uniqueidentifier  NOT NULL,
    [PollAnswer_Answer_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Votes'
CREATE TABLE [dbo].[Votes] (
    [Id] uniqueidentifier  NOT NULL,
    [User_Id] int  NOT NULL,
    [Answer_Id] uniqueidentifier  NOT NULL,
    [Poll_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] uniqueidentifier  NOT NULL,
    [Sender] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [SentAt] datetime  NOT NULL,
    [IsRead] bit  NOT NULL,
    [Inbox_Id] int  NOT NULL
);
GO

-- Creating table 'Inboxes'
CREATE TABLE [dbo].[Inboxes] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Polls_DatePoll'
CREATE TABLE [dbo].[Polls_DatePoll] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Polls_TextPoll'
CREATE TABLE [dbo].[Polls_TextPoll] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Answers_DateAnswer'
CREATE TABLE [dbo].[Answers_DateAnswer] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Answers_TextAnswer'
CREATE TABLE [dbo].[Answers_TextAnswer] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Polls'
ALTER TABLE [dbo].[Polls]
ADD CONSTRAINT [PK_Polls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Votes'
ALTER TABLE [dbo].[Votes]
ADD CONSTRAINT [PK_Votes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Inboxes'
ALTER TABLE [dbo].[Inboxes]
ADD CONSTRAINT [PK_Inboxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Polls_DatePoll'
ALTER TABLE [dbo].[Polls_DatePoll]
ADD CONSTRAINT [PK_Polls_DatePoll]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Polls_TextPoll'
ALTER TABLE [dbo].[Polls_TextPoll]
ADD CONSTRAINT [PK_Polls_TextPoll]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Answers_DateAnswer'
ALTER TABLE [dbo].[Answers_DateAnswer]
ADD CONSTRAINT [PK_Answers_DateAnswer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Answers_TextAnswer'
ALTER TABLE [dbo].[Answers_TextAnswer]
ADD CONSTRAINT [PK_Answers_TextAnswer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Creator_Id] in table 'Polls'
ALTER TABLE [dbo].[Polls]
ADD CONSTRAINT [FK_UserPoll]
    FOREIGN KEY ([Creator_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPoll'
CREATE INDEX [IX_FK_UserPoll]
ON [dbo].[Polls]
    ([Creator_Id]);
GO

-- Creating foreign key on [PollAnswer_Answer_Id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_PollAnswer]
    FOREIGN KEY ([PollAnswer_Answer_Id])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PollAnswer'
CREATE INDEX [IX_FK_PollAnswer]
ON [dbo].[Answers]
    ([PollAnswer_Answer_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Votes'
ALTER TABLE [dbo].[Votes]
ADD CONSTRAINT [FK_UserVote]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserVote'
CREATE INDEX [IX_FK_UserVote]
ON [dbo].[Votes]
    ([User_Id]);
GO

-- Creating foreign key on [Answer_Id] in table 'Votes'
ALTER TABLE [dbo].[Votes]
ADD CONSTRAINT [FK_VoteAnswer]
    FOREIGN KEY ([Answer_Id])
    REFERENCES [dbo].[Answers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VoteAnswer'
CREATE INDEX [IX_FK_VoteAnswer]
ON [dbo].[Votes]
    ([Answer_Id]);
GO

-- Creating foreign key on [Poll_Id] in table 'Votes'
ALTER TABLE [dbo].[Votes]
ADD CONSTRAINT [FK_PollVote]
    FOREIGN KEY ([Poll_Id])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PollVote'
CREATE INDEX [IX_FK_PollVote]
ON [dbo].[Votes]
    ([Poll_Id]);
GO

-- Creating foreign key on [Inbox_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_InboxUser]
    FOREIGN KEY ([Inbox_Id])
    REFERENCES [dbo].[Inboxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InboxUser'
CREATE INDEX [IX_FK_InboxUser]
ON [dbo].[Users]
    ([Inbox_Id]);
GO

-- Creating foreign key on [Inbox_Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_InboxNotification]
    FOREIGN KEY ([Inbox_Id])
    REFERENCES [dbo].[Inboxes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InboxNotification'
CREATE INDEX [IX_FK_InboxNotification]
ON [dbo].[Notifications]
    ([Inbox_Id]);
GO

-- Creating foreign key on [Id] in table 'Polls_DatePoll'
ALTER TABLE [dbo].[Polls_DatePoll]
ADD CONSTRAINT [FK_DatePoll_inherits_Poll]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Polls_TextPoll'
ALTER TABLE [dbo].[Polls_TextPoll]
ADD CONSTRAINT [FK_TextPoll_inherits_Poll]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Answers_DateAnswer'
ALTER TABLE [dbo].[Answers_DateAnswer]
ADD CONSTRAINT [FK_DateAnswer_inherits_Answer]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Answers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Answers_TextAnswer'
ALTER TABLE [dbo].[Answers_TextAnswer]
ADD CONSTRAINT [FK_TextAnswer_inherits_Answer]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Answers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------