﻿using System;
using System.Collections.Generic;
using System.Data;
using DbUp.Engine.Output;

namespace DbUp.Engine.Transactions
{
    /// <summary>
    /// Allows you to run an operation with a managed connection
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        /// Tells the connection manager it is starting an operation
        /// </summary>
        IDisposable OperationStarting(IUpgradeLog upgradeLog, List<SqlScript> executedScripts);

        /// <summary>
        /// Execute a lambda with the connection managed by the connection manager (i.e transactions, reusing connections etc)
        /// </summary>
        /// <param name="action">Action to execute</param>
        void ExecuteCommandsWithManagedConnection(Action<Func<IDbCommand>> action);

        /// <summary>
        /// Execute a lambda with the connection managed by the connection manager (i.e transactions, reusing connections etc)
        /// </summary>
        /// <param name="actionWithResult">Action to execute</param>
        T ExecuteCommandsWithManagedConnection<T>(Func<Func<IDbCommand>, T> actionWithResult);

        /// <summary>
        /// Specifies the transaction strategy
        /// </summary>
        TransactionMode TransactionMode { get; set; }

        /// <summary>
        /// Container which holds sql statments for specific implementation.
        /// </summary>
        SqlStatementsContainer SqlContainer { get; }

        /// <summary>
        /// Specifies whether the db script output should be logged
        /// </summary>
        bool IsScriptOutputLogged { get; set; }

        /// <summary>
        /// Scripts often have multiple statements which have to be executed in their own commands.
        /// 
        /// For example, MSSQL splits on GO, SQLite splits on ; etc.
        /// </summary>
        IEnumerable<string> SplitScriptIntoCommands(string scriptContents);

        /// <summary>
        /// Set journalingTableName and scheme for specific sql container containing sql statements. 
        /// </summary>
        /// <param name="journalingTable">Name of journaling table</param>
        /// <param name="scheme">Scheme of Journaling table</param>
        void SetSqlContainerParameters(string journalingTable, string scheme);
        
        /// <summary>
        /// Tries to connect to the database.
        /// </summary> 
        bool TryConnect(IUpgradeLog upgradeLog, out string errorMessage);
    }
}