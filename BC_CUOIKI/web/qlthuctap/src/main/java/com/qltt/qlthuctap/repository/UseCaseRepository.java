package com.qltt.qlthuctap.repository;

import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ConnectionCallback;
import org.springframework.stereotype.Repository;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

/** Executes only procedures declared in the registry; procedure names never come from user input. */
@Repository
public class UseCaseRepository {
    private final JdbcTemplate jdbc;

    public UseCaseRepository(JdbcTemplate jdbc) { this.jdbc = jdbc; }

    public List<Map<String, Object>> call(String procedure, List<String> parameters, Map<String, Object> values) {
        StringBuilder sql = new StringBuilder("EXEC dbo.").append(procedure);
        if (!parameters.isEmpty()) {
            sql.append(' ');
            for (int i = 0; i < parameters.size(); i++) {
                if (i > 0) sql.append(", ");
                String name = parameters.get(i);
                sql.append('@').append(name).append("=?");
            }
        }
        return jdbc.execute((ConnectionCallback<List<Map<String, Object>>>) connection -> {
            try (PreparedStatement statement = connection.prepareStatement(sql.toString())) {
                for (int i = 0; i < parameters.size(); i++) {
                    statement.setObject(i + 1, values.get(parameters.get(i)));
                }
                boolean hasRows = statement.execute();
                while (!hasRows && statement.getUpdateCount() != -1) hasRows = statement.getMoreResults();
                if (!hasRows) return List.of();
                try (ResultSet rs = statement.getResultSet()) {
                    ResultSetMetaData md = rs.getMetaData();
                    List<Map<String, Object>> rows = new ArrayList<>();
                    while (rs.next()) {
                        Map<String, Object> row = new LinkedHashMap<>();
                        for (int i = 1; i <= md.getColumnCount(); i++) row.put(md.getColumnLabel(i), rs.getObject(i));
                        rows.add(row);
                    }
                    return rows;
                }
            }
        });
    }
}
