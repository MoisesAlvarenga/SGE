// Função genérica para requisições JSON
async function fetchJson(url, opts) {
    const r = await fetch(url, opts);
    if (!r.ok) {
        const err = await r.text();
        throw new Error(`Erro ${r.status}: ${err}`);
    }
    return r.json();
}

// ========================
//   LISTAGENS
// ========================

async function listarCursos() {
    const cursos = await fetchJson('/api/cursos');
    const selAluno = document.getElementById('alunoCurso');
    const selTurma = document.getElementById('turmaCurso');
    selAluno.innerHTML = '';
    selTurma.innerHTML = '';

    cursos.forEach(c => {
        const texto = `${c.nome} (${c.codigo})`;
        const opt1 = new Option(texto, c.id);
        const opt2 = new Option(texto, c.id);
        selAluno.add(opt1);
        selTurma.add(opt2);
    });

    document.getElementById('cursos').innerText = JSON.stringify(cursos, null, 2);
}

async function listarProfessores() {
    const professores = await fetchJson('/api/professores');
    const sel = document.getElementById('turmaProf');
    sel.innerHTML = '';

    professores.forEach(p => sel.add(new Option(p.nome, p.id)));
    document.getElementById('professores').innerText = JSON.stringify(professores, null, 2);
}

async function listarAlunos() {
    const alunos = await fetchJson('/api/alunos');
    const sel = document.getElementById('avaliAluno');
    sel.innerHTML = '';

    alunos.forEach(a => sel.add(new Option(`${a.nome} - ${a.matricula || ''}`, a.id)));
    document.getElementById('alunos').innerText = JSON.stringify(alunos, null, 2);
}

async function listarTurmas() {
    const turmas = await fetchJson('/api/cursos/turmas');
    const sel = document.getElementById('avaliTurma');
    sel.innerHTML = '';

    turmas.forEach(t => {
        const texto = `${t.codigo} - ${(t.cursoNome || '')}`;
        sel.add(new Option(texto, t.id));
    });

    document.getElementById('turmas').innerText = JSON.stringify(turmas, null, 2);
}

// ========================
//   CRIAÇÕES
// ========================

async function criarCurso() {
    const dto = {
        nome: document.getElementById('cursoNome').value,
        codigo: document.getElementById('cursoCodigo').value,
        cargaHoraria: parseInt(document.getElementById('cursoCarga').value || '0'),
        tipo: document.getElementById('cursoTipo').value,
        salaOuPlataforma: document.getElementById('cursoExtra').value
    };

    await fetchJson('/api/cursos', {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify(dto)
    });

    await listarCursos();
}

async function criarProfessor() {
    const dto = {
        nome: document.getElementById('profNome').value,
        especialidade: document.getElementById('profEsp').value,
        registro: document.getElementById('profReg').value
    };

    await fetchJson('/api/professores', {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify(dto)
    });

    await listarProfessores();
}

async function criarAluno() {
    const dto = {
        nome: document.getElementById('alunoNome').value,
        matricula: document.getElementById('alunoMat').value,
        cursoId: document.getElementById('alunoCurso').value || null
    };

    await fetchJson('/api/alunos', {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify(dto)
    });

    await listarAlunos();
}

async function criarTurma() {
    const dto = {
        codigo: document.getElementById('turmaCodigo').value,
        professorId: document.getElementById('turmaProf').value || null,
        cursoId: document.getElementById('turmaCurso').value || null
    };

    await fetchJson('/api/turmas', {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify(dto)
    });

    await listarTurmas();
}

// ========================
//   AVALIAÇÕES E RELATÓRIOS
// ========================

async function registrarAvaliacao() {
    const dto = {
        alunoId: document.getElementById('avaliAluno').value,
        turmaId: document.getElementById('avaliTurma').value,
        nota: parseFloat(document.getElementById('avaliNota').value || '0'),
        descricao: document.getElementById('avaliDesc').value
    };

    await fetchJson('/api/avaliacoes', {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify(dto)
    });

    alert('✅ Avaliação registrada com sucesso!');
}

async function gerarRelatorios() {
    const alunos = await fetchJson('/api/alunos/relatorios');
    const professores = await fetchJson('/api/professores/relatorios');
    const cursos = await fetchJson('/api/cursos/relatorios');

    const resultado = [
        '--- Alunos ---',
        ...alunos,
        '',
        '--- Professores ---',
        ...professores,
        '',
        '--- Cursos ---',
        ...cursos
    ].join('\n');

    document.getElementById('relatorios').innerText = resultado;
}

// ========================
//   CARGA INICIAL
// ========================
(async () => {
    await listarCursos();
    await listarProfessores();
    await listarAlunos();
    await listarTurmas();
})();

console.log("app.js carregado!");
