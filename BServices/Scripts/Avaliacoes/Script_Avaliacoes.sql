-- Cotações
alter table cabeccompras add CDU_PO_DataPedido datetime
alter table cabeccompras add CDU_PO_DataEmissao datetime

alter table cabeccompras add CDU_PO_TeveInquetaca bit default 0
alter table cabeccompras add CDU_PO_TemFlexibilidade bit default 1
alter table cabeccompras add CDU_PO_Inqueitacao_Data datetime null
alter table cabeccompras add CDU_PO_Inqueitacao_DataResposta datetime null
alter table cabeccompras add CDU_PO_Inqueitcao_TeveResposta72 bit default 1

--ECF
alter table cabeccompras add CDU_ECF_PrecoIgualCot bit default 1
alter table cabeccompras add CDU_ECF_TemPagamentoMais30Dias bit default 0
alter table cabeccompras add CDU_ECF_TemDescontoConsedido bit default 0

-- VGR
alter table cabeccompras add CDU_VGR_TemEntregaCompleta bit default 1
alter table cabeccompras add CDU_VGR_TemPrazoCumprido bit default 1
alter table cabeccompras add CDU_VGR_TemEspecificacaoSemDanos bit default 1
alter table cabeccompras add CDU_VGR_TemDocumentacaoCompleta bit default 1